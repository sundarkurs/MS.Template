using Microsoft.AspNetCore.Mvc;
using WSA.Microservice.Template.Application.Commands.Config;
using WSA.Microservice.Template.Application.DTO;
using WSA.Microservice.Template.Application.Queries.Config;

namespace WSA.Microservice.Template.Web.Controllers.Api
{
    public class ConfigController : BaseApiController
    {
        private readonly ILogger<ConfigController> _logger;

        public ConfigController(ILogger<ConfigController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await Mediator.Send(new GetAllConfigs.Query()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await Mediator.Send(new GetConfig.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ConfigRequest configRequest)
        {
            var response = await Mediator.Send(new CreateConfig.Command { Config = configRequest });
            return Ok(response);
        }

    }
}
