﻿using Microsoft.EntityFrameworkCore;
using WSA.Microservice.Template.Application.Interfaces.Repositories;
using WSA.Microservice.Template.Domain.Entities;
using WSA.Microservice.Template.Persistence.Contexts;

namespace WSA.Microservice.Template.Persistence.Repositories
{
    public class ConfigRepository : BaseRepository<Config>, IConfigRepository
    {
        private readonly DbSet<Config> _configs;

        public ConfigRepository(TemplateContext dbContext) : base(dbContext)
        {
            _configs = dbContext.Set<Config>();
        }

        public Task<bool> IsNameUniqueAsync(string name)
        {
            return _configs.AllAsync(p => p.Name != name);
        }
    }
}
