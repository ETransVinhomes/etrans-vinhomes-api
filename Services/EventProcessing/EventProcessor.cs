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
                default:
                    break;
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
                    System.Console.WriteLine("--> Platform Published Event Detected");
                    return EventType.UserCreation;
                default:
                    System.Console.WriteLine("--> Undetermined Event Detected");
                    return EventType.Undefined;
            }
        }
    }
}
