using System;
namespace ServiceHub.API.Application.Models.FeatureConfigurations
{
	public class HealthLinkInterfaceConfiguration : FeatureConfiguraiton
	{
		public HealthLinkInterfaceConfiguration()
		{
		}

        public override bool IsEnabled { get; set; }
    }
}

