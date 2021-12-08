using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using WSA.Microservice.Template.Inbound.WebJob.Interfaces;

namespace WSA.Microservice.Template.Inbound.WebJob
{
    public class Functions
    {
        private readonly ILogger<Functions> _logger;
        private readonly IConfigImporter _configImporter;

        public Functions(ILogger<Functions> logger, IConfigImporter configImporter)
        {
            _logger = logger;
            _configImporter = configImporter;
        }

        [NoAutomaticTrigger]
        public async Task RunAsync(TextWriter writer, CancellationToken cancellationToken)
        {
            writer.WriteLine($"{nameof(RunAsync)} started at {DateTime.UtcNow}");
            await _configImporter.ProcessAsync();
        }
    }
}
