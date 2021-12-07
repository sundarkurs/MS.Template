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

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);


var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();


/**** Add services to the container. ****/

builder.Services.AddSingleton<ITelemetryInitializer>(new CloudRoleNameTelemetryInitializer(configuration.GetValue<string>("appInsights:WebApp:CloudRoleName")));
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.InstrumentationKey = configuration.GetValue<string>("appInsights:iKey");
});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
     .AddMicrosoftIdentityWebApp(options =>
     {
         configuration.Bind("Authentication:AzureAdB2C", options);
         options.Events.OnRedirectToIdentityProvider += OnRedirectToIdentityProvider;
         options.Events.OnTokenValidated += OnTokenValidated;
         options.Events.OnRemoteFailure += OnRemoteFailure;
     });

Task OnRemoteFailure(RemoteFailureContext arg)
{
    throw new NotImplementedException();
}

builder.Services.AddAuthorization(options =>
{
    //if (Configuration.GetValue<bool>("EnableTokenPage"))
    //    options.AddPolicy("AllowTokenView", policy => policy.RequireClaim("Role", "AllowTokenView"));
    options.AddPolicy("AllowTokenView", policy => policy.Requirements.Add(new ViewTokenRequirement(configuration.GetValue<bool>("EnableTokenPage"))));
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddMicrosoftIdentityUI();
builder.Services.AddSingleton<IAuthorizationHandler, ViewTokenAuthorizationHandler>();

var app = builder.Build();

/**** Configure the HTTP request pipeline. ****/

app.Use((context, next) =>
{
    context.Request.Scheme = "https";
    return next();
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();





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