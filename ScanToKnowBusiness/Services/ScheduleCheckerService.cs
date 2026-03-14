using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScanToKnowDataAccess.Repositories;

public class ScheduleCheckerService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IServiceProvider _serviceProvider;

    public ScheduleCheckerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Run every 1 minute (adjust interval as needed)
        _timer = new Timer(CheckSchedules, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        return Task.CompletedTask;
    }

    private async void CheckSchedules(object state)
    {
        using var scope = _serviceProvider.CreateScope();
        var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        // Get all schedules
        var schedules = await userRepo.GetAllSchedulesRepoAsync();
        var now = DateTime.UtcNow.AddHours(8); //local testing addhours(8) remove on deploy

        foreach (var schedule in schedules)
        {
            // If schedule has ended but ScheduleEnd is null
            if (schedule.ScheduleEndTime <= now && schedule.ScheduleEnd == null)
            {
                var scheduleCheckerRequest = new UpdateStartOrEndRequest
                {
                    ScheduleId = schedule.ScheduleId.Value,

                };

                await userRepo.UpdateScheduleEndOnlyAsync(scheduleCheckerRequest);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}