using System.Threading.Tasks;

namespace WSA.Microservice.Template.WebJob.InboundProcess.Interfaces
{
    public interface IConfigImporter
    {
        Task ProcessAsync();
    }
}
