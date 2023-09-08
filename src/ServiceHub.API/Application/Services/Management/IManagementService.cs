namespace ServiceHub.API.Application.Services.Management
{
    public interface IManagementService
    {
		public ValueTask LoadProfilesAndRunFeatrues();
        public bool StopFeatrue(string profileName, string featureName);
    }
}

