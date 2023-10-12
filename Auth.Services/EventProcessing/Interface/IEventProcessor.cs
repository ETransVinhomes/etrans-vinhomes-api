namespace Auth.Services.EventProcessing.Interface;
public interface IEventProcessor
{
    public void ProcessEvent(string message);
}