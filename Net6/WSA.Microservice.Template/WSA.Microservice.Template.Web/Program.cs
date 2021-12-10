using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.Security.Claims;
using WSA.Microservice.Template.Application;
using WSA.Microservice.Template.Infrastructure.Persistence;
using WSA.Microservice.Template.Web.Auth;
using WSA.Microservice.Template.Web.Extensions;
using WSA.Microservice.Template.Web.Logger;
using WSA.Microservice.Template.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

ConfigureConfiguration(builder);

ConfigureServices(builder.Services, builder.Configuration);

builder.Services.AddSwaggerExtension();

var app = builder.Build();

ConfigureMiddleware(app);

ConfigureEndpoints(app);

app.Run();


#region Configure Methods

void ConfigureConfiguration(WebApplicationBuilder builder)
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

    builder.Logging.AddApplicationInsights();

    builder.Logging.AddLog4Net(Environment.CurrentDirectory + @"\log4net.config", true);
}

void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
{
    services.AddApplicationServices();
    services.AddPersistenceServices(configuration);

    services.AddSingleton<ITelemetryInitializer>(new CloudRoleNameTelemetryInitializer(configuration.GetValue<string>("appInsights:WebApp:CloudRoleName")));

    services.AddApplicationInsightsTelemetry(options =>
    {
        options.InstrumentationKey = configuration.GetValue<string>("appInsights:iKey");
    });

    services.Configure<CookiePolicyOptions>(options =>
    {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });

    services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
         .AddMicrosoftIdentityWebApp(options =>
         {
             configuration.Bind("Authentication:AzureAdB2C", options);
             options.Events.OnRedirectToIdentityProvider += IdentityEventHandlers.OnRedirectToIdentityProvider;
             options.Events.OnTokenValidated += IdentityEventHandlers.OnTokenValidated;
             options.Events.OnRemoteFailure += IdentityEventHandlers.OnRemoteFailure;
         });

    services.AddAuthorization(options =>
    {
        options.AddPolicy("AllowTokenView", policy => policy.Requirements.Add(new ViewTokenRequirement(configuration.GetValue<bool>("EnableTokenPage"))));
    });

    services.AddControllersWithViews();

    services.AddRazorPages().AddMicrosoftIdentityUI();

    services.AddSingleton<IAuthorizationHandler, ViewTokenAuthorizationHandler>();
};

void ConfigureMiddleware(WebApplication app)
{
    app.Use((context, next) =>
    {
        context.Request.Scheme = "https";
        return next();
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
        app.UseSwaggerExtension();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseCookiePolicy();

    app.UseAuthentication();

    app.UseAuthorization();

    app.UseErrorHandlingMiddleware();
};

void ConfigureEndpoints(WebApplication app)
{
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
};

#endregion
