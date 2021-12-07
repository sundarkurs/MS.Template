using Microsoft.EntityFrameworkCore;
using WSA.Microservice.Template.Application.Interfaces.Repositories;
using WSA.Microservice.Template.Persistence.Contexts;

namespace WSA.Microservice.Template.Persistence.Repositories
{
    public class ConfigurationRepository<T> : BaseRepository<T>, IConfigurationRepository<T> where T : class
    {
        private readonly DbSet<T> _configurations;

        public ConfigurationRepository(TemplateContext dbContext) : base(dbContext)
        {
            _configurations = dbContext.Set<T>();
        }
    }
}
