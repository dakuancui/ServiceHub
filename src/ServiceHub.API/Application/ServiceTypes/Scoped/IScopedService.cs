namespace ServiceHub.API.Application.ServiceTypes.Scoped
{
    public interface IScopedService
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }
}
