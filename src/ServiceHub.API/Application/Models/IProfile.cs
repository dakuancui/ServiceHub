using ServiceHub.API.Application.Models.FeatureConfigurations;

namespace ServiceHub.API.Application.Models
{
    public interface IProfile
    {
        public string Name { get; set; }
        public string DatabaseName { get; set; }
        public IEnumerable<IFeatureConfiguraiton> FeatureConfigurations { get; set; }
    }
}
