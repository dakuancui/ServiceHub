namespace ServiceHub.Core.Application.Models.FeatureConfiguration
{
    public interface IFeatureConfiguraiton
    {
        public string FeatrueName { get; set; }
        public bool Enabled { get; set; }
        public string Config { get; set; }
    }
}
