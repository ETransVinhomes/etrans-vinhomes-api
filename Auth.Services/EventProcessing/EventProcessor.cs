using System.Text.Json;
using Auth.Domains.Enums;
using Auth.Services.AsyncDataServices;
using Auth.Services.EventProcessing.Interface;
using Auth.Services.Repositories;
using Auth.Services.ViewModels;
using Auth.Services.ViewModels.PublishedAccountModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Services.EventProcessing;
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
            case EventType.UserModified:
                // ToDo Code
                System.Console.WriteLine(message);
                UpdateEntity(message);
                break;
            case EventType.UserCreation:
                System.Console.WriteLine($"User Creation not catch On AuthService! Do Nothing");
                break;
            case EventType.UserDelete:
                System.Console.WriteLine($"User Delete! No Action");
                break;
            default:
                break;
        }
    }
    private async void UpdateEntity(string message)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var authRepo = scope.ServiceProvider.GetRequiredService<IAuthRepository>();
            _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            var userModel = JsonSerializer.Deserialize<UserPublishedModel>(message) ?? throw new Exception($"Deserilize failed for {message}");
            try
            {
                var user = await authRepo.GetUserByIdAsync(userModel.Id) ?? throw new Exception($"--> Not found User with Id: {userModel.Id}");
                user.PhoneNumber = userModel.PhoneNumber;
                user.Name = userModel.Name;
                await authRepo.UpdateUserAsync(user);
                System.Console.WriteLine($"--> info: Update user successfully at Auth Service");
                
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
            case nameof(EventType.UserModified):
                System.Console.WriteLine("--> User ModifiedEventDetected");
                return EventType.UserModified;
            case nameof(EventType.UserCreation):
                System.Console.WriteLine("--> User Creation Event Detected");
                return EventType.UserCreation;
            case nameof(EventType.UserDelete): 
                return EventType.UserDelete;
            default:
                System.Console.WriteLine("--> Undetermined Event Detected");
                return EventType.Undefined;
        }
    }
}
