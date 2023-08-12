namespace ServiceHub.API.Application.ServiceTypes.Scoped
{
    public abstract class ScopedService : IScopedService
    {
        private int _executionCount;
        private readonly ILogger<ScopedService> _logger;

        public ScopedService(
            ILogger<ScopedService> logger) =>
            _logger = logger;

        public abstract Task DoAsync();

        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ++_executionCount;

                _logger.LogInformation(
                    "{ServiceName} working, execution count: {Count}",
                    nameof(ScopedService),
                    _executionCount);
                await DoAsync();
            }
        }
    }
}
