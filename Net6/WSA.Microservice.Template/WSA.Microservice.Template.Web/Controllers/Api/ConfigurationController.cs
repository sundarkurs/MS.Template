using Microsoft.AspNetCore.Mvc;

namespace WSA.Microservice.Template.Web.Controllers.Api
{
    public class ConfigurationController : BaseApiController
    {
        private readonly ILogger<ConfigurationController> _logger;

        public ConfigurationController(ILogger<ConfigurationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok("Success");
        }
    }
}
