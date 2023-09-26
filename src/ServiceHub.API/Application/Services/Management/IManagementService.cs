namespace ServiceHub.API.Application.Services.Management
{
    public interface IManagementService
    {
		public ValueTask LoadProfilesAndRunFeatures();
        public ValueTask AddProfileAndRunFeatures();
        public bool StopFeatrue(string profileName, string featureName);
        public string CurrentStatus();
    }
}

