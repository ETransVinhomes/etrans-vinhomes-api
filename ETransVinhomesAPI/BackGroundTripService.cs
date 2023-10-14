using Hangfire;
using Services.Services.Interfaces;

namespace ETransVinhomesAPI;
public class BackGroundTripService 
{
    private readonly ITripService _tripService;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IRecurringJobManager _recurringJob;
    public BackGroundTripService(ITripService tripService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
    {
        _backgroundJobClient = backgroundJobClient;
        _recurringJob = recurringJobManager;
        _tripService = tripService;
    }
     public void EnqueueJob()
    {
        _backgroundJobClient.Enqueue(() => Console.WriteLine("Background job executed."));
    }

    public void ScheduleRecurringJob()
    {
        _recurringJob.AddOrUpdate<ITripService>("Check Trip Started", x => x.CheckTripStarted(), Cron.Minutely);
    }
}