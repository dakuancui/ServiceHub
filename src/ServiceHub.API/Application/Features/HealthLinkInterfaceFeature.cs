using ServiceHub.API.Application.Consumers;
using ServiceHub.API.Application.Models.FeatureConfigurations;
using ServiceHub.API.Application.Triggers;

namespace ServiceHub.API.Application.Features
{
    public class HealthLinkInterfaceFeature<C> : Feature where C : HealthLinkInterfaceConfiguration
    {
        private readonly ILogger<HealthLinkInterfaceFeature<HealthLinkInterfaceConfiguration>> _logger;
        protected IList<ITrigger> _triggers { get; set; } = new List<ITrigger>();
        protected IList<IConsumer> _consumers { get; set; } = new List<IConsumer>();

        public HealthLinkInterfaceFeature(ILogger<HealthLinkInterfaceFeature<HealthLinkInterfaceConfiguration>> logger)
        {
            _logger = logger;
            _triggers.Add(new FileSystemChangeTrigger(_logger));
            _consumers.Add(new FileConsumer(_logger));
        }

        public override IEnumerable<ITrigger> Triggers
            => _triggers.AsEnumerable();


        public override IEnumerable<IConsumer> Consumers
            => _consumers.AsEnumerable();

        public override string Name => "HealthLinkInterface feature";

        public override void Apply()
        {
            _triggers.First().Start((FileSystemEventHandler)_consumers.First().ConsumeHandler);
        }
    }
}
