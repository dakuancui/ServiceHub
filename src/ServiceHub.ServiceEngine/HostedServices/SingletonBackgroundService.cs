using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceHub.ServiceEngine.ServiceTypes.Singleton;

namespace ServiceHub.ServiceEngine.HostedServices
{
    public abstract class SingletonBackgroundService : BackgroundService
    {
        private readonly ILogger<SingletonBackgroundService> _logger;

        public SingletonBackgroundService(ILogger<SingletonBackgroundService> logger) =>
        ( _logger) = (logger);

        public abstract Task DoWorkAsync(CancellationToken cancellationToken);
        //public abstract Task OnStart(CancellationToken cancellationToken);
        //public abstract Task OnStop(CancellationToken cancellationToken);
        //public abstract void OnDispose();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await DoWorkAsync(stoppingToken);
                //while (!stoppingToken.IsCancellationRequested)
                //{
                //    //TODO something here when canceled. 
                //    //await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                //}
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

    //    public override Task StartAsync(CancellationToken cancellationToken)
    //    {
    //        OnStart(cancellationToken);
    //        return base.StartAsync(cancellationToken);
    //    }

    //    public override Task StopAsync(CancellationToken cancellationToken)
    //    {
    //        OnStop(cancellationToken);
    //        return base.StopAsync(cancellationToken);
    //    }

    //    public override void Dispose()
    //    {
    //        OnDispose();
    //        base.Dispose();
    //    }
    }
}
