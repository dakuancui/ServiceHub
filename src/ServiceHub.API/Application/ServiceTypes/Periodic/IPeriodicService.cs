namespace ServiceHub.API.Application.ServiceTypes.Periodic
{
    public interface IPeriodicService
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }
}
