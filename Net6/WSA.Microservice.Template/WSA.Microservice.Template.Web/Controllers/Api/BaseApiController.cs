using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WSA.Microservice.Template.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class BaseApiController : ControllerBase
    {
    }
}
