﻿using Microsoft.Extensions.Logging;
using ServiceHub.Core.Application.Consumer;
using ServiceHub.Core.Application.Models.FeatureConfiguration;
using ServiceHub.Core.Application.Models.FeatureControl;
using ServiceHub.Core.Application.Models.Statistic;
using ServiceHub.Core.Application.Trigger;

namespace ServiceHub.Core.Application.Feature
{
    public abstract class Feature<C> : IFeature<C> , IObserver<FeatureCommand>, IDisposable
        where C : IFeatureConfiguraiton
	{
        private readonly ILogger<Feature<IFeatureConfiguraiton>> _logger;
        protected IList<ITrigger> Triggers { get; set; } = new List<ITrigger>();
        protected IList<IConsumer> Consumers { get; set; } = new List<IConsumer>();
        private IDisposable _unsubscriber;

        public Feature(ILogger<Feature<IFeatureConfiguraiton>> logger)
		{
			_logger = logger;
		}

        public abstract string ProfileName { get; }
		public abstract string Name { get; }

        public virtual void Subscribe(IObservable<FeatureCommand> provider)
        {
            if (provider != null)
                _unsubscriber = provider.Subscribe(this);
        }

        public abstract void Apply(C featureConfig, CancellationToken cancellationToken);

        public void OnCompleted()
        {
            _logger.LogInformation("{0} {1} command subscribe on completed.", this.ProfileName, this.Name);
            this.Unsubscribe();
        }

        public void OnError(Exception error)
        {
            _logger.LogInformation("{0} {1} command subscibe on error.", this.ProfileName, this.Name);
        }

        public void OnNext(FeatureCommand value)
        {
            _logger.LogInformation("A new {0} command has received for {1} - {2}.", value.Command, this.ProfileName, this.Name);
            if (value.ProfileName == this.ProfileName
                && value.FeatureName == this.Name
                && value.Command == "stop")
            {
                foreach (var trigger in Triggers)
                {
                    trigger.Stop();
                    _logger.LogInformation("Feature {0} from profile {1} stopped.", this.Name, this.ProfileName);
                    var profile = Statistics.RunningStatistics.FirstOrDefault(x => x.Value.ProfileName == this.ProfileName).Value;
                    if (profile != null)
                    {
                        profile.FeaturesStatistics.First(x => x.FeatureName == Name).Status = "Stopped";
                        Statistics.RunningStatistics[profile.Id] = profile;
                    }
                }
                Dispose();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }
    }
}

