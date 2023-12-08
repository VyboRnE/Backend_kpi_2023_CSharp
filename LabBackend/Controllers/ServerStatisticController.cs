using Microsoft.AspNetCore.Mvc;

namespace LabBackend.Controllers
{
    [ApiController]
    [Route("/healthcheck")]
    public class ServerStatisticController: ControllerBase
    {
        private readonly ILogger<ServerStatisticController> _logger;

        public ServerStatisticController(ILogger<ServerStatisticController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult HealthCheck()
        {
            var health = new
            {
                Status = "Ok",
                Time = DateTime.Now,
            };
            return Ok(health);
        }
    }
}
