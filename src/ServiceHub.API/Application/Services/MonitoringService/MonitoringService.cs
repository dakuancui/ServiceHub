using System;
using ServiceHub.API.Application.Features;
using ServiceHub.API.Application.Models.FeatureConfigurations;
using ServiceHub.API.Application.Monitor;
using ServiceHub.API.Application.Services.Profile;
using ServiceHub.ServiceEngine.ServiceTypes.QueueService;

namespace ServiceHub.API.Application.Services.MonitoringService
{
	public class MonitoringService : IMonitoringService
	{
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger<MonitoringService> _logger;
        private readonly CancellationToken _cancellationToken;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProfileService _profileService;

        public MonitoringService(
            IBackgroundTaskQueue taskQueue,
            ILogger<MonitoringService> logger,
            IHostApplicationLifetime applicationLifetime,
            IServiceProvider serviceProvider,
            IProfileService profileService)
        {
            _taskQueue = taskQueue;
            _logger = logger;
            _cancellationToken = applicationLifetime.ApplicationStopping;
            _serviceProvider = serviceProvider;
            _profileService = profileService;
        }

        public async ValueTask AddProfileAndRunFeatures()
        { 
            await _taskQueue.QueueBackgroundWorkItemAsync(BuildWorkItemAsync);
        }

        private async ValueTask BuildWorkItemAsync(CancellationToken token)
        {
            var guid = Guid.NewGuid();
            _logger.LogInformation("Queued a profile all featrues {Guid} is starting.", guid);

            var profiles = _profileService.GetProfiles();
            foreach(var profile in profiles)
            {
                foreach(var featureConfig in profile.FeatureConfigurations)
                {
                    if (featureConfig.Enabled)
                    {
                        var featureName = featureConfig.FeatrueName;
                        if (featureName == "HealthLinkInterfaceFeature")
                        {
                            var logger = _serviceProvider.GetRequiredService<ILogger<HealthLinkInterfaceFeature<HealthLinkInterfaceConfiguration>>>();
                            var service = new HealthLinkInterfaceFeature<HealthLinkInterfaceConfiguration>(logger);
                            service.Apply();
                        }
                    }
                }
            }
        }

    }
}

