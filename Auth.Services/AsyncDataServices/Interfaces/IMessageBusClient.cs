using Auth.Services.ViewModels.PublishedAccountModels;

namespace Auth.Services.AsyncDataServices.Interfaces;
public interface IMessageBusClient 
{
    void PublishNewAccount(UserPublishedModel userPublishedModel);
}