using System;
using ServiceHub.API.Application.Models;
using ServiceHub.API.Application.Models.FeatureConfigurations;

namespace ServiceHub.API.Application.Services.Profile
{
	public class ProfileService : IProfileService
	{
		public ProfileService()
		{
		}

		public IEnumerable<IProfile> GetProfiles()
		{
			var mockProfile = new ServiceHub.API.Application.Models.Profile
			{
				Name = "TestProfile-1",
				DatabaseName = "Profile-1-Db",
				FeatureConfigurations = new List<IFeatureConfiguraiton>
				{
					new FeatureConfiguraiton
					{
						FeatrueName = "HealthLinkInterfaceFeature",
						Enabled = true,
						Config = "{\"WatchDirectPath\": \"/Users/DakuanC/Dakuan.asb/spikes/ServiceHub/temp\",   \"FileFitler\" : \"*.*\"} "
                    }
				}.AsEnumerable()
			};
			return new List<IProfile> { mockProfile }.AsEnumerable();
		}
    }
}

