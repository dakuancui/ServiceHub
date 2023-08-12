using Microsoft.AspNetCore.Mvc;
using ServiceHub.ServiceEngine.HostedServices;
using ServiceHub.ServiceEngine.ServiceTypes.Periodic;

namespace ServiceHub.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceHubController : ControllerBase
    {
        private readonly ILogger<ServiceHubController> _logger;
        private readonly PeriodicBackgroundService _periodicBackgroundService;
        public ServiceHubController(ILogger<ServiceHubController> logger, PeriodicBackgroundService periodicBackgroundService)
        {
            _logger = logger;
            _periodicBackgroundService = periodicBackgroundService;
        }

        [HttpGet(Name = "PeriodicService")]
        public PeriodicHostedServiceState Get()
        {
            return new PeriodicHostedServiceState(_periodicBackgroundService.IsEnabled);
        }

        [HttpPatch(Name = "PeriodicService")]
        public IActionResult Patch([FromBody] PeriodicHostedServiceState state)
        {
            _periodicBackgroundService.IsEnabled = state.IsEnabled;
            return Accepted();
        }

    }
}