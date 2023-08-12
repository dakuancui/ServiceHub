namespace ServiceHub.ServiceEngine.ServiceTypes.Scoped
{
    public interface IScopedService
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }
}
