using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WSA.Microservice.Template.Application.Interfaces.Repositories;
using WSA.Microservice.Template.Persistence.Contexts;
using WSA.Microservice.Template.Persistence.Repositories;

namespace WSA.Microservice.Template.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {  
            // Database context
            services.AddDbContext<TemplateContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(TemplateContext).Assembly.FullName)));

            // Repositories
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IConfigRepository, ConfigRepository>();
        }
    }
}
