using WSA.Microservice.Template.Domain.Entities;

namespace WSA.Microservice.Template.Application.Common.Interfaces.Repositories
{
    public interface ITodoRepository : IBaseRepository<Todo>
    {
        Task<bool> IsTitleUniqueAsync(string code);
    }
}
