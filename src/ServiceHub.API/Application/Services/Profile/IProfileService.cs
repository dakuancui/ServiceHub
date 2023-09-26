using ServiceHub.Core.Application.Models;

namespace ServiceHub.API.Application.Services.Profile
{
    public interface IProfileService
	{
		public IEnumerable<IProfile> GetProfiles();
	}
}

