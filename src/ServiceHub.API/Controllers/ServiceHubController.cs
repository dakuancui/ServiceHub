using Microsoft.AspNetCore.Mvc;
using ServiceHub.API.Application.Features;
using ServiceHub.API.Application.Services;
using ServiceHub.API.Application.Services.MonitoringService;
using ServiceHub.ServiceEngine.HostedServices;
using ServiceHub.ServiceEngine.ServiceTypes.Periodic;

namespace ServiceHub.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceHubController : ControllerBase
    {
        private readonly ILogger<ServiceHubController> _logger;
        //private readonly PeriodicBackgroundService _periodicBackgroundService;
        //private readonly IServiceProvider _serviceProvider;
        private readonly IMonitoringService _monitoringService;

        public ServiceHubController(ILogger<ServiceHubController> logger,
            //PeriodicBackgroundService periodicBackgroundService,
            //IServiceProvider serviceProvider
            IMonitoringService monitoringService
            )
        {
            _logger = logger;
            //_periodicBackgroundService = periodicBackgroundService;
            //_serviceProvider = serviceProvider;
            _monitoringService = monitoringService;
        }

        //[HttpGet(Name = "PeriodicService")]
        //public PeriodicHostedServiceState Get()
        //{
        //    return new PeriodicHostedServiceState(_periodicBackgroundService.IsEnabled);
        //}

        //[HttpPatch(Name = "Service")]
        //public IActionResult Patch(string servicetype, [FromBody] PeriodicHostedServiceState state)
        //{
        //    if (servicetype == "periodic")
        //    {
        //        _periodicBackgroundService.IsEnabled = state.IsEnabled;
        //    }
        //    return Accepted();
        //}

        //[HttpPatch(Name = "ServiceStart")]
        //public IActionResult ServiceStart([FromBody] bool start)
        //{
        //    if (start)
        //    {
        //        //_serviceCollection.AddHostedService<HealthLinkInterfaceService<IFeature>>();
        //    }
        //    return Accepted();
        //}

        //[HttpGet(Name = "LoadProfiles")]
        //public IActionResult LoadProfiles()
        //{
        //    return Accepted();
        //}

        [HttpPatch(Name = "AddProfile")]
        public async Task<IActionResult> AddProfile()
        {
            await _monitoringService.AddProfileAndRunFeatures();
            return Accepted();
        }

    }
}