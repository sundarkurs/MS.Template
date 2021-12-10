using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;

namespace WSA.Microservice.Template.Web.Auth
{
    public static class IdentityEventHandlers
    {
        public static Task OnRedirectToIdentityProvider(RedirectContext context)
        {
            return Task.CompletedTask;
        }

        public static Task OnTokenValidated(TokenValidatedContext context)
        {
            var identity =
                new ClaimsIdentity(
                    new List<Claim>
                    {
                        new Claim("id_token", context.ProtocolMessage.IdToken),
                        new Claim("access_token", context.ProtocolMessage.AccessToken ?? string.Empty),
                        new Claim("refresh_token", context.ProtocolMessage.RefreshToken ?? string.Empty)
                    });
            context.Principal.AddIdentity(identity);
            return Task.CompletedTask;
        }

        public static Task OnRemoteFailure(RemoteFailureContext arg)
        {
            throw new NotImplementedException();
        }
    }
}
