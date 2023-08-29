using ServiceHub.API.Application.Models;
using ServiceHub.ServiceEngine.HostedServices;
using ServiceHub.API.Application.Features;

namespace ServiceHub.API.Application.Services
{
    public class HealthLinkInterfaceService<F> : SingletonBackgroundService
        //where P : IProfile
        where F : IFeature
    {
        private readonly ILogger<HealthLinkInterfaceService<F>> _logger;
        private readonly F _feature;

        public HealthLinkInterfaceService(F feature, ILogger<HealthLinkInterfaceService<F>> logger) : base(logger)
        {
            _logger = logger;
            _feature = feature;
        }

        public override Task DoWorkAsync(CancellationToken cancellationToken)
        {
            var triggers = _feature.Triggers;
            _logger.LogInformation($"HealthLinkInterface Service doing the work. {_feature.Name}");
            _feature.Apply();
            return Task.CompletedTask;
        }
    }
}
