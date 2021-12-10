using Microsoft.EntityFrameworkCore;
using WSA.Microservice.Template.Application.Common.Interfaces.Repositories;
using WSA.Microservice.Template.Domain.Entities;
using WSA.Microservice.Template.Infrastructure.Persistence.Contexts;

namespace WSA.Microservice.Template.Infrastructure.Persistence.Repositories
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        private readonly DbSet<Todo> _todos;

        public TodoRepository(TemplateContext dbContext) : base(dbContext)
        {
            _todos = dbContext.Set<Todo>();
        }

        public Task<bool> IsTitleUniqueAsync(string title)
        {
            return _todos.AllAsync(p => p.Title != title);
        }
    }
}
