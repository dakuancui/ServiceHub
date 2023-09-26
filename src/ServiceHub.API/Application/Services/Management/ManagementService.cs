using ServiceHub.API.Application.Features;
using ServiceHub.API.Application.Services.Profile;
using ServiceHub.Core.Application.Feature.Control;
using ServiceHub.Core.Application.Models.FeatureConfiguration;
using ServiceHub.Core.Application.Models.FeatureControl;
using ServiceHub.Core.Application.Models.Statistic;
using ServiceHub.Core.HostedServices.ServiceTypes.QueueService;
using System.Text.Json;

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

        public async ValueTask LoadProfilesAndRunFeatures()
        {
            await _taskQueue.QueueBackgroundWorkItemAsync(BuildWorkItemAsync);
        }

        private async ValueTask BuildWorkItemAsync(CancellationToken token)
        {
            var profiles = _profileService.GetProfiles();
            foreach(var profile in profiles)
            {
                var profileId = Guid.NewGuid();
                _logger.LogInformation("Queuing a profile's {Guid} all featrues are starting.", profileId);
                var profileStatistic = new ProfileStatistics
                {
                    Id = profileId.ToString(),
                    Status = "Loading",
                    ProfileName = profile.Name
                };
                var featuresStatistic = new List<FeatureStatistics>();
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
                            featuresStatistic.Add(new FeatureStatistics { FeatureName = featureName, Status = "Running" });
                        }
                    }
                }
                profileStatistic.Status = "Loaded";
                profileStatistic.FeaturesStatistics = featuresStatistic;
                Statistics.RunningStatistics.Add(profileId.ToString(), profileStatistic);
                _logger.LogInformation("Queued a profile's {Guid} all featrues are started.", profileId);
            }
        }

        public async ValueTask AddProfileAndRunFeatures()
        {
            await _taskQueue.QueueBackgroundWorkItemAsync(BuildMockProfileItem);
        }

        private async ValueTask BuildMockProfileItem(CancellationToken token)
        {
            var mockProfile1 = new Core.Application.Models.Profile
            {
                Name = "TestProfile-3",
                DatabaseName = "Profile-3-Db",
                FeatureConfigurations = new List<IFeatureConfiguraiton>
                {
                    new FeatureConfiguraiton
                    {
                        FeatrueName = "HealthLinkInterfaceFeature",
                        Enabled = true,
                        Config = "{\"WatchDirectPath\": \"/Users/DakuanC/Dakuan.asb/spikes/ServiceHub/temp2\",   \"FileFitler\" : \"*.*\"} "
                    }
                }.AsEnumerable()
            };
            //var mockProfile = new List<IProfile> { mockProfile1 }.AsEnumerable();
            var profileId = Guid.NewGuid();
            _logger.LogInformation("Queuing a profile's {Guid} all featrues are starting.", profileId);
            var profileStatistic = new ProfileStatistics
            {
                Id = profileId.ToString(),
                Status = "Loading",
                ProfileName = mockProfile1.Name
            };
            var featuresStatistic = new List<FeatureStatistics>();
            foreach (var featureConfig in mockProfile1.FeatureConfigurations)
            {
                if (featureConfig.Enabled)
                {
                    var featureName = featureConfig.FeatrueName;
                    if (featureName == "HealthLinkInterfaceFeature")
                    {
                        var logger = _serviceProvider.GetRequiredService<ILogger<HealthLinkInterfaceFeature<IFeatureConfiguraiton>>>();
                        var feature = new HealthLinkInterfaceFeature<IFeatureConfiguraiton>(logger, mockProfile1.Name, featureName);
                        feature.Subscribe(_featureCommand);
                        feature.Apply(featureConfig, token);
                        featuresStatistic.Add(new FeatureStatistics { FeatureName = featureName, Status = "Running" });
                    }
                }
            }
            profileStatistic.Status = "Loaded";
            profileStatistic.FeaturesStatistics = featuresStatistic;
            Statistics.RunningStatistics.Add(profileId.ToString(), profileStatistic);
            _logger.LogInformation("Queued a profile's {Guid} all featrues are started.", profileId);
        }

        public string CurrentStatus()
        {
            return JsonSerializer.Serialize(Statistics.RunningStatistics);
        }
    }
}

