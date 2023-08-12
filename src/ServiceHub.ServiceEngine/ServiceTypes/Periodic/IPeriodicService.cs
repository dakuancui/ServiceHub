namespace ServiceHub.ServiceEngine.ServiceTypes.Periodic
{
    public interface IPeriodicService
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }
}
