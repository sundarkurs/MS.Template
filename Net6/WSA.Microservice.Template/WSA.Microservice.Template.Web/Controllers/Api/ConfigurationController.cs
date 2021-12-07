using Microsoft.AspNetCore.Mvc;
using WSA.Microservice.Template.Application.Queries.Configuration;

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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await Mediator.Send(new GetConfiguration.Query { Id = id }));
        }
    }
}
