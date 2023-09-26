using ServiceHub.Core.Application.Models.FeatureConfiguration;

namespace ServiceHub.Core.Application.Models
{
    public interface IProfile
    {
        public string Name { get; set; }
        public string DatabaseName { get; set; }
        public IEnumerable<IFeatureConfiguraiton> FeatureConfigurations { get; set; }
    }
}
