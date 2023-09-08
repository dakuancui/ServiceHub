using ServiceHub.API.Application.Models.FeatureConfigurations;

namespace ServiceHub.API.Application.Features
{
    public interface IFeature<C> where C : IFeatureConfiguraiton
	{
        public string ProfileName { get; }
        public string Name { get; }
        public void Apply(C featureConfig, CancellationToken cancellationToken);
    }
}

