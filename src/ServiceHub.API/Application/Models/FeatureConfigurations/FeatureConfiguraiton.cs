using System;
namespace ServiceHub.API.Application.Models.FeatureConfigurations
{
    public class FeatureConfiguraiton : IFeatureConfiguraiton
    {
        public string FeatrueName { get; set; }

        public bool Enabled { get; set; }

        public string Config { get; set; }
    }
}

