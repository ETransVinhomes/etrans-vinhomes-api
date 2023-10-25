using AutoMapper;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Services.EventProcessing.Interfaces;
using Services.ViewModels.AsyncDataModels;
using System.Text.Json;

namespace Services.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private IMapper _mapper = default!;
        private readonly IServiceScopeFactory _scopeFactory = default!;
        public EventProcessor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case EventType.UserCreation:
                    // ToDo Code
                    System.Console.WriteLine(message);
                    AddEntity(message);

                    break;
                case EventType.UserModified:
                    System.Console.WriteLine($"--> info: User Modified At Core Service! Do Nothing");
                    break;
                case EventType.UserDelete:
                    DeleteUser(message);
                    break;
                default:
                    break;
            }
        }
        private async void DeleteUser(string message)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var userModel = JsonSerializer.Deserialize<UserDeleteModel>(message) ?? throw new Exception($"Deserilize exception for {message}");
                try
                {
                    switch (userModel.Role)
                    {
                        case nameof(RoleEnum.PROVIDER):
                            var user = await unitOfWork.ProviderRepository.FindByField(x => x.ExternalId == userModel.Id) ?? throw new Exception($"Not found Provider with ExternalId: {userModel.Id}");
                            unitOfWork.ProviderRepository.SoftRemove(user);
                            break;
                        case nameof(RoleEnum.CUSTOMER):
                            var userCustomer = await unitOfWork.CustomerRepository.FindByField(x => x.ExternalId == userModel.Id) ?? throw new Exception($"Not found Customer with ExternalId: {userModel.Id}");
                            unitOfWork.CustomerRepository.SoftRemove(userCustomer);
                            break;
                        case nameof(RoleEnum.DRIVER):
                            var userDriver = await unitOfWork.DriverRepository.FindByField(x => x.ExternalId == userModel.Id) ?? throw new Exception($"Not found Driver with ExternalId: {userModel.Id}");
                            unitOfWork.DriverRepository.SoftRemove(userDriver);
                            break;
                    }
                    await unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        private async void AddEntity(string message)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                var userModel = JsonSerializer.Deserialize<UserPublishedModel>(message) ?? throw new Exception($"Deserilize failed for {message}");
                try
                {
                    switch (userModel.Role)
                    {
                        case "PROVIDER":
                            System.Console.WriteLine($"--> Info: Received Message Create Provider");
                            await unitOfWork.ProviderRepository.AddAsync(new Domain.Entities.Provider
                            {
                                ExternalId = userModel.Id,
                                Name = userModel.Name,
                                PhoneNumber = userModel.PhoneNumber
                            });
                            break;
                        case "CUSTOMER":
                            System.Console.WriteLine($"--> Info: Received Message Create Customer");
                            await unitOfWork.CustomerRepository.AddAsync(new Domain.Entities.Customer
                            {
                                ExternalId = userModel.Id,
                                Email = userModel.Email,
                                Name = userModel.Name,
                                PhoneNumber = userModel.PhoneNumber
                            });
                            break;
                        case "DRIVER":
                            System.Console.WriteLine($"--> Info: Received Message Create Driver");
                            await unitOfWork.DriverRepository.AddAsync(new Domain.Entities.Driver
                            {

                                ExternalId = userModel.Id,
                                PhoneNumber = userModel.PhoneNumber,
                                Name = userModel.Name
                            });

                            break;
                        default:
                            throw new Exception($"Create User With Role is not supported");

                    }

                    await unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        private EventType DetermineEvent(string notificationMessage)
        {
            System.Console.WriteLine("--> Determining Event");
            var eventType = JsonSerializer.Deserialize<GenericEventModel>(notificationMessage) ?? throw new Exception($"Can not Deserialize object");
            switch (eventType.Event)
            {
                case nameof(EventType.UserCreation):
                    System.Console.WriteLine("--> User Creation Event Detected");
                    return EventType.UserCreation;
                case nameof(EventType.UserModified):
                    System.Console.WriteLine("--> UserModified Event Detected");
                    return EventType.UserModified;
                case nameof(EventType.UserDelete):
                    return EventType.UserDelete;
                default:
                    System.Console.WriteLine("--> Undetermined Event Detected");
                    return EventType.Undefined;
            }
        }
    }
}
