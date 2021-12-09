using MediatR;
using Microsoft.Extensions.Logging;
using WSA.Microservice.Template.Application.Queries.Config;
using WSA.Microservice.Template.WebJob.InboundProcess.Interfaces;

namespace WSA.Microservice.Template.WebJob.InboundProcess.Services
{
    public class ConfigImporter : IConfigImporter
    {
        private readonly ILogger<ConfigImporter> _logger;
        private IMediator _mediator;

        public ConfigImporter(ILogger<ConfigImporter> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task ProcessAsync()
        {
            _logger.LogInformation("Process started");

            var response = await _mediator.Send(new GetAllConfigs.Query());

            return;
        }
    }
}
