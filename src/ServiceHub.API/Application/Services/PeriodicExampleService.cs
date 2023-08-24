using ServiceHub.ServiceEngine.ServiceTypes.Periodic;

namespace ServiceHub.API.Application.Services
{
    public class PeriodicExampleService : PeriodicService
    {
        private readonly ILogger<PeriodicExampleService> _logger;
        public PeriodicExampleService(ILogger<PeriodicExampleService> logger) : base(logger)
        {
            _logger = logger;
        }

        public override async Task DoAsync()
        {
            _logger.LogInformation(
                "Periodic Service did something.");
            await Task.Delay(100);
        }
    }
}
