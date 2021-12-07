using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.Security.Claims;
using WSA.Microservice.Template.Web.Auth;
using WSA.Microservice.Template.Web.Logger;

var builder = WebApplication.CreateBuilder(args);

ConfigureConfiguration(builder);

ConfigureServices(builder.Services, builder.Configuration);

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
             options.Events.OnRedirectToIdentityProvider += OnRedirectToIdentityProvider;
             options.Events.OnTokenValidated += OnTokenValidated;
             options.Events.OnRemoteFailure += OnRemoteFailure;
         });

    services.AddAuthorization(options =>
    {
        //if (Configuration.GetValue<bool>("EnableTokenPage"))
        //    options.AddPolicy("AllowTokenView", policy => policy.RequireClaim("Role", "AllowTokenView"));
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
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseCookiePolicy();

    app.UseAuthentication();

    app.UseAuthorization();
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

#region Azure Authentication Handlers

Task OnRedirectToIdentityProvider(RedirectContext context)
{
    //context.ProtocolMessage.DomainHint = "wsa.com";
    //context.ProtocolMessage.RedirectUri
    return Task.CompletedTask;
}

Task OnTokenValidated(TokenValidatedContext context)
{
    var identity =
        new System.Security.Claims.ClaimsIdentity(
            new List<Claim>
            {
                        new System.Security.Claims.Claim("id_token", context.ProtocolMessage.IdToken),
                        new System.Security.Claims.Claim("access_token", context.ProtocolMessage.AccessToken ?? string.Empty),
                        new System.Security.Claims.Claim("refresh_token", context.ProtocolMessage.RefreshToken ?? string.Empty)
            });
    context.Principal.AddIdentity(identity);
    return Task.CompletedTask;
}

Task OnRemoteFailure(RemoteFailureContext arg)
{
    throw new NotImplementedException();
}

#endregion