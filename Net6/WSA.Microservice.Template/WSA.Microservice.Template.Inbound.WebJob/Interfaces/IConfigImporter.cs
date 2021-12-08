using System.Threading.Tasks;

namespace WSA.Microservice.Template.Inbound.WebJob.Interfaces
{
    public interface IConfigImporter
    {
        Task ProcessAsync();
    }
}
