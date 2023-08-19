using ServiceHub.API.Application.Models;
using ServiceHub.ServiceEngine.HostedServices;
//using ServiceHub.ServiceEngine.ServiceTypes.Singleton;

namespace ServiceHub.API.Application.Features.Services
{
    public class HealthLinkInterfaceService<P, F> : SingletonBackgroundService where P : IProfile where F : IFeature 
    {
        private readonly ILogger<HealthLinkInterfaceService<P,F>> _logger;

        public HealthLinkInterfaceService(ILogger<HealthLinkInterfaceService<P,F>> logger) : base(logger)
        {
            _logger = logger;
        }

        public override Task DoWorkAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("HealthLinkInterface Service doing the work.");
            return Task.CompletedTask;
        }
    }
}
