namespace ServiceHub.API.Application.ServiceTypes.Singleton
{
    public interface ISingletonService
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }
}
