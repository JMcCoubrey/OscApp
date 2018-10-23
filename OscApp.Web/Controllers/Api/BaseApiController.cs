using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OscApp.Web.Filters;

namespace OscApp.Web.Controllers.Api
{
    [Authorize]
    [GeneralExceptionFilter]
    public class BaseApiController : Controller
    {
    }
}
