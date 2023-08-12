namespace ServiceHub.API.Application.ServiceTypes.Periodic
{
    public abstract class PeriodicService : IPeriodicService
    {
        private readonly ILogger<PeriodicService> _logger;

        public PeriodicService(
            ILogger<PeriodicService> logger) =>
            _logger = logger;

        public abstract Task DoAsync();

        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            await DoAsync();
        }
    }

    public record PeriodicHostedServiceState(bool IsEnabled);
}
