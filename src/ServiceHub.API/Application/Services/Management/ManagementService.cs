using ServiceHub.API.Application.Features;
using ServiceHub.API.Application.Models.FeatureControl;
using ServiceHub.API.Application.Models.FeatureConfigurations;
using ServiceHub.API.Application.Providers;
using ServiceHub.API.Application.Services.Profile;
using ServiceHub.ServiceEngine.ServiceTypes.QueueService;

namespace ServiceHub.API.Application.Services.Management
{
    public class ManagementService : IManagementService
    {
        private readonly IQueueTaskService _taskQueue;
        private readonly ILogger<ManagementService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProfileService _profileService;
        private readonly IObservable<FeatureCommand> _featureCommand;
        public ManagementService(
            IQueueTaskService taskQueue,
            ILogger<ManagementService> logger,
            IServiceProvider serviceProvider,
            IProfileService profileService,
            IObservable<FeatureCommand> featureCommand)
        {
            _taskQueue = taskQueue;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _profileService = profileService;
            _featureCommand = featureCommand;
        }

        public bool StopFeatrue(string profileName, string featureName)
        {
            var featureCommand = _serviceProvider.GetService<IObservable<FeatureCommand>>() as FeatureCommandPublisher;
            featureCommand?.SendCommand(new FeatureCommand { ProfileName = profileName, FeatureName = featureName, Command = "stop" });
            return true;
        }

        public async ValueTask LoadProfilesAndRunFeatrues()
        {
            await _taskQueue.QueueBackgroundWorkItemAsync(BuildWorkItemAsync);
        }

        private async ValueTask BuildWorkItemAsync(CancellationToken token)
        {
            var profiles = _profileService.GetProfiles();
            foreach(var profile in profiles)
            {
                var profileId = Guid.NewGuid();
                _logger.LogInformation("Queue a profile's {Guid} all featrues are starting.", profileId);
                foreach (var featureConfig in profile.FeatureConfigurations)
                {
                    if (featureConfig.Enabled)
                    {
                        var featureName = featureConfig.FeatrueName;
                        if (featureName == "HealthLinkInterfaceFeature")
                        {
                            var logger = _serviceProvider.GetRequiredService<ILogger<HealthLinkInterfaceFeature<IFeatureConfiguraiton>>>();
                            var feature = new HealthLinkInterfaceFeature<IFeatureConfiguraiton>(logger, profile.Name, featureName);
                            feature.Subscribe(_featureCommand);
                            feature.Apply(featureConfig, token);
                        }
                    }
                }
                _logger.LogInformation("Queued a profile's {Guid} all featrues are started.", profileId);
            }
        }

    }
}

