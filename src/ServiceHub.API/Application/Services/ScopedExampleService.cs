using ServiceHub.ServiceEngine.ServiceTypes.Scoped;

namespace ServiceHub.API.Application.Services
{
    public class ScopedExampleService : ScopedService
    {
        private readonly ILogger<ScopedExampleService> _logger;
        public ScopedExampleService(ILogger<ScopedExampleService> logger) : base(logger)
        {
            _logger = logger;
        }

        public override async Task DoAsync()
        {
            _logger.LogInformation($"Scoped service processing.");
            await Task.Delay(10_000);
        }
    }
}
