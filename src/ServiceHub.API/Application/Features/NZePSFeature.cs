using ServiceHub.API.Application.Consumers;
using ServiceHub.API.Application.Models.FeatureConfigurations;
using ServiceHub.API.Application.Triggers;
using System.Text.Json;
using ServiceHub.Core.Application.Feature;
using ServiceHub.Core.Application.Models.FeatureConfiguration;

namespace ServiceHub.API.Application.Features
{
    public class NZePSFeature<C> : Feature<C> where C : IFeatureConfiguraiton
    {
        private readonly ILogger<NZePSFeature<IFeatureConfiguraiton>> _logger;
        private readonly string _profileName;
        private readonly string _featureName;

        public NZePSFeature(ILogger<NZePSFeature<IFeatureConfiguraiton>> logger,
            string profileName, string featureName
            ) : base(logger)
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
                var configObject = JsonSerializer.Deserialize<NZePSConfiguration>(config);
                var url = configObject?.NZePSUrl;
                Triggers.Add(new HttpRequestTrigger(_logger, url));
                Consumers.Add(new FileConsumer(_logger));
                Triggers.First().Start((FileSystemEventHandler)Consumers.First().ConsumeHandler, cancellationToken);
            }
        }
    }
}

