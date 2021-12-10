using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using WSA.Microservice.Template.WebJob.InboundProcess.Interfaces;

namespace WSA.Microservice.Template.WebJob.InboundProcess
{
    public class Functions
    {
        private readonly ILogger<Functions> _logger;
        private readonly ITodoImporter _todoImporter;

        public Functions(ILogger<Functions> logger, ITodoImporter todoImporter)
        {
            _logger = logger;
            _todoImporter = todoImporter;
        }

        [NoAutomaticTrigger]
        public async Task RunAsync(TextWriter writer, CancellationToken cancellationToken)
        {
            writer.WriteLine($"{nameof(RunAsync)} started at {DateTime.UtcNow}");
            await _todoImporter.ProcessAsync();
        }
    }
}
