using Services.ViewModels.AsyncDataModels;

namespace Services.AsyncDataServices.Interfaces;
public interface IMessageBusClient
{
    void PublishUpdateAccount(UserPublishedModel model);
}