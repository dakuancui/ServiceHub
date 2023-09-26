using ServiceHub.Core.Application.Models;
using ServiceHub.Core.Application.Models.FeatureConfiguration;

namespace ServiceHub.API.Application.Services.Profile
{
    public class ProfileService : IProfileService
	{
		public ProfileService()
		{
		}

		public IEnumerable<IProfile> GetProfiles()
		{
			var mockProfile1 = new Core.Application.Models.Profile
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
            var mockProfile2 = new Core.Application.Models.Profile
            {
                Name = "TestProfile-2",
                DatabaseName = "Profile-2-Db",
                FeatureConfigurations = new List<IFeatureConfiguraiton>
                {
                    new FeatureConfiguraiton
                    {
                        FeatrueName = "HealthLinkInterfaceFeature",
                        Enabled = true,
                        Config = "{\"WatchDirectPath\": \"/Users/DakuanC/Dakuan.asb/spikes/ServiceHub/temp1\",   \"FileFitler\" : \"*.*\"} "
                    }
                }.AsEnumerable()
            };
            return new List<IProfile> { mockProfile1, mockProfile2 }.AsEnumerable();
		}
    }
}

