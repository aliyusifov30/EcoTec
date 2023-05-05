using Microsoft.AspNetCore.Mvc;

namespace EcoTech.MVC.Controllers
{
	public class AboutUsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
