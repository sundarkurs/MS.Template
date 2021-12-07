using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WSA.Microservice.Template.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
