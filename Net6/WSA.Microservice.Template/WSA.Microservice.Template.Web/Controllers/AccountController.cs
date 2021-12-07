using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace WSA.Microservice.Template.Web.Controllers
{
    public class AccountController : BaseMvcController
    {
        public IActionResult SignOut(string redirect_uri)
        {
            redirect_uri ??= "/";

            return SignOut(new AuthenticationProperties
            {
                RedirectUri = redirect_uri ?? "/"
            }, CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
