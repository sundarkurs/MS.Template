using Microsoft.AspNetCore.Mvc.Filters;
using WSA.Microservice.Template.Web.Constants;

namespace WSA.Microservice.Template.Web.Attributes
{
    public sealed class TokenAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context != null)
            {
                var logger = context.HttpContext.RequestServices.GetService<ILogger<TokenAuthorizeAttribute>>();
                var headers = context.HttpContext.Request.Headers;

                headers.TryGetValue(AuthConstants.Header.Authorization, out var authorization);

                if (string.IsNullOrEmpty(authorization))
                {
                    logger.LogError(AuthConstants.Messages.AuthorizationNotFound,
                        new UnauthorizedAccessException(AuthConstants.Messages.AuthorizationNotFound));
                    throw new UnauthorizedAccessException(AuthConstants.Messages.AuthorizationNotFound);
                }

                var token = authorization.ToString().Split(' ');
                if (token.Length != 2 || !token[0].Equals(AuthConstants.Header.Bearer, StringComparison.InvariantCultureIgnoreCase))
                {
                    logger.LogError(AuthConstants.Messages.JwtTokenNotFound,
                        new UnauthorizedAccessException(AuthConstants.Messages.JwtTokenNotFound));
                    throw new UnauthorizedAccessException(AuthConstants.Messages.JwtTokenNotFound);
                }

                var jwtToken = token[1];

                try
                {
                    var claims = WSA.Identity.Token.ValidateToken(jwtToken);
                    context.HttpContext.User = claims;
                }
                catch (Exception ex)
                {
                    throw new UnauthorizedAccessException(ex.Message);
                }
            }
        }
    }
}
