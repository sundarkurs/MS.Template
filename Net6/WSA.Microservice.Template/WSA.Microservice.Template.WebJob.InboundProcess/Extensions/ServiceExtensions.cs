using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WSA.Microservice.Template.WebJob.InboundProcess.Interfaces;

namespace WSA.Microservice.Template.WebJob.InboundProcess.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IConfigImporter, Services.ConfigImporter>();
        }

        public static void AddConfigurations(this IServiceCollection services, HostBuilderContext context)
        {
            //services.Configure<AppSettings>(context.Configuration.GetSection("AppSettings"));
            //services.Configure<ConnectionStrings>(context.Configuration.GetSection("ConnectionStrings"));
            //services.Configure<MailSettings>(context.Configuration.GetSection("MailSettings"));
            //services.Configure<StorageSettings>(context.Configuration.GetSection("StorageSettings"));
        }
    }
}
