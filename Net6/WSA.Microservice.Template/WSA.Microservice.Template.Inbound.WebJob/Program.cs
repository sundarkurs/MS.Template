using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WSA.Microservice.Template.Application;
using WSA.Microservice.Template.Inbound.WebJob;
using WSA.Microservice.Template.Inbound.WebJob.Extensions;
using WSA.Microservice.Template.Persistence;

IConfiguration Configuration;

var builder = new ConfigurationBuilder();

BuildConfig(builder);

Configuration = builder.Build();

var host = Host.CreateDefaultBuilder()
                .ConfigureWebJobs(b =>
                {
                    b.AddAzureStorageCoreServices()
                    .AddAzureStorageBlobs()
                    .AddTimers();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddServices();
                    services.AddConfigurations(context);

                    services.AddApplicationServices();
                    services.AddPersistenceServices(Configuration);
                })
                .Build();

using (host)
{
    var jobHost = host.Services.GetService(typeof(IJobHost)) as JobHost;
    await host.StartAsync();
    await jobHost.CallAsync(nameof(Functions.RunAsync));
    await host.StopAsync();
}

void BuildConfig(IConfigurationBuilder builder)
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
}