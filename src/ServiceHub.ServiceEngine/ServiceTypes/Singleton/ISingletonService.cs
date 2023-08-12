namespace ServiceHub.ServiceEngine.ServiceTypes.Singleton
{
    public interface ISingletonService
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }
}
