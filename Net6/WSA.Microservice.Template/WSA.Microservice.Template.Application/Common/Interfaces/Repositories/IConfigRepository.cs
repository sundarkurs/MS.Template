using WSA.Microservice.Template.Domain.Entities;

namespace WSA.Microservice.Template.Application.Common.Interfaces.Repositories
{
    public interface IConfigRepository : IBaseRepository<Config>
    {
        Task<bool> IsNameUniqueAsync(string code);
    }
}
