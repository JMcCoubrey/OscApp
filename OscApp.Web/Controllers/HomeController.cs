using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OscApp.Model;

namespace OscApp.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

		public HomeController()
		{
		}

		public IActionResult Index()
        {
			return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
