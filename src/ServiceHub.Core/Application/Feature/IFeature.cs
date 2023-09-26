using ServiceHub.Core.Application.Models.FeatureConfiguration;

namespace ServiceHub.Core.Application.Feature
{
    public interface IFeature<C> where C : IFeatureConfiguraiton
	{
        public string ProfileName { get; }
        public string Name { get; }
        public void Apply(C featureConfig, CancellationToken cancellationToken);
    }
}

