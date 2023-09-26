using System.Text.Json;
using ServiceHub.API.Application.Consumers;
using ServiceHub.API.Application.Models.FeatureConfigurations;
using ServiceHub.API.Application.Triggers;
using ServiceHub.Core.Application.Feature;
using ServiceHub.Core.Application.Models.FeatureConfiguration;

namespace ServiceHub.API.Application.Features
{
    public class HealthLinkInterfaceFeature<C> : Feature<C> where C : IFeatureConfiguraiton 
    {
        private readonly ILogger<HealthLinkInterfaceFeature<IFeatureConfiguraiton>> _logger;
        private readonly string _profileName;
        private readonly string _featureName;

        public HealthLinkInterfaceFeature(
            ILogger<HealthLinkInterfaceFeature<IFeatureConfiguraiton>> logger,
            string profileName, string featureName
            ): base(logger)
        {
            _logger = logger;
            _profileName = profileName;
            _featureName = featureName;
        }

        public override string ProfileName => _profileName;
        public override string Name => _featureName;

        public override void Apply(C featureConfig, CancellationToken cancellationToken)
        {
            var config = featureConfig.Config;
            if (!string.IsNullOrWhiteSpace(config))
            { 
                var configObject = JsonSerializer.Deserialize<HealthLinkInterfaceConfiguration>(config);
                var watchPath = configObject?.WatchDirectPath;
                var fileFilter = configObject?.FileFitler;
                Triggers.Add(new FileSystemChangeTrigger(_logger, watchPath, fileFilter));
                Consumers.Add(new FileConsumer(_logger));
                Triggers.First().Start((FileSystemEventHandler)Consumers.First().ConsumeHandler, cancellationToken);
            }
        }
    }
}
