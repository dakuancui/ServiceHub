namespace ServiceHub.Core.HostedServices.ServiceTypes.Periodic
{
    public interface IPeriodicService
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }
}
