
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceHub.ServiceEngine.ServiceTypes.Singleton;

namespace ServiceHub.ServiceEngine.HostedServices
{
    public class SingletonBackgroundService : BackgroundService
    {
        private readonly ISingletonService _singletonService;
        private readonly ILogger<SingletonBackgroundService> _logger;

        public SingletonBackgroundService(
        ISingletonService singletonService,
        ILogger<SingletonBackgroundService> logger) =>
        (_singletonService, _logger) = (singletonService, logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await _singletonService.DoWorkAsync(stoppingToken);
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
            catch (TaskCanceledException)
            {
                // When the stopping token is canceled, for example, a call made from services.msc,
                // we shouldn't exit with a non-zero exit code. In other words, this is expected...
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);
                Environment.Exit(1);
            }
        }
    }
}
