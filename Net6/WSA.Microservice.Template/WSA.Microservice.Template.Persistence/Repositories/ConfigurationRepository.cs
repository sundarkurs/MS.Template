using Microsoft.EntityFrameworkCore;
using WSA.Microservice.Template.Application.Interfaces.Repositories;
using WSA.Microservice.Template.Domain.Entities;
using WSA.Microservice.Template.Persistence.Contexts;

namespace WSA.Microservice.Template.Persistence.Repositories
{
    public class ConfigurationRepository : BaseRepository<Config>, IConfigurationRepository
    {
        private readonly DbSet<Config> _configurations;

        public ConfigurationRepository(TemplateContext dbContext) : base(dbContext)
        {
            _configurations = dbContext.Set<Config>();
        }
    }
}
