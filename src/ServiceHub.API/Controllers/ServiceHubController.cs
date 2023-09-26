using Microsoft.AspNetCore.Mvc;
using ServiceHub.API.Application.Services.Management;

namespace ServiceHub.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceHubController : ControllerBase
    {
        private readonly ILogger<ServiceHubController> _logger;
        private readonly IManagementService _manageMentService;

        public ServiceHubController(ILogger<ServiceHubController> logger,
            IManagementService managementService
            )
        {
            _logger = logger;
            _manageMentService = managementService;
        }

        [HttpPatch("AddProfileRunFeatures", Name = nameof(AddProfileRunFeatures))]
        public async Task<IActionResult> AddProfileRunFeatures()
        {
            await _manageMentService.AddProfileAndRunFeatures();
            return Accepted();
        }

        [HttpPatch("LoadProfilesRunFeatures",Name = nameof(LoadProfilesAndRunFeatures))]
        public async Task<IActionResult> LoadProfilesAndRunFeatures()
        {
            await _manageMentService.LoadProfilesAndRunFeatures();
            return Accepted();
        }

        [HttpPatch("StopFeature", Name = nameof(StopProfileFeature))]
        public IActionResult StopProfileFeature([FromQuery] string profileName, string featureName)
        {
            if (_manageMentService.StopFeatrue(profileName, featureName))
                return Accepted();
            else
                return Conflict();
        }

        [HttpGet("CurrentStatus", Name = nameof(CurrentStatus))]
        public IActionResult CurrentStatus()
        {
            return Ok(_manageMentService.CurrentStatus());
        }
    }
}