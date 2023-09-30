using ServiceHub.Core.Application.Feature;
using ServiceHub.Core.Application.Models.FeatureConfiguration;
using ServiceHub.Core.Application.Trigger;

namespace ServiceHub.API.Application.Triggers
{
    public class HttpRequestTrigger : ITrigger
	{
        private readonly string _url;
        private readonly ILogger<IFeature<IFeatureConfiguraiton>> _logger;

        public HttpRequestTrigger(ILogger<IFeature<IFeatureConfiguraiton>> logger, string url)
		{
            _logger = logger;
            _url = url;
		}

        public void Start(object eventHandler, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}

