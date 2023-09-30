using Microsoft.AspNetCore.Mvc;

namespace ServiceHub.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FunctionController : ControllerBase
	{
        private readonly ILogger<FunctionController> _logger;

        public FunctionController(ILogger<FunctionController> logger)
		{
            _logger = logger;
		}

        [HttpPost("CallNZePS", Name = nameof(CallNZePS))]
        public IActionResult CallNZePS(string profileName, string featureName)
        {
            return Accepted();
        }
    }
}

