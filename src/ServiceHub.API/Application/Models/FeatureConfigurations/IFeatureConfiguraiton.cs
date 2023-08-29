namespace ServiceHub.API.Application.Models.FeatureConfigurations
{
    public interface IFeatureConfiguraiton
    {
        public string FeatrueName { get; set; }
        public bool Enabled { get; set; }
        public string Config { get; set; }
    }
}
