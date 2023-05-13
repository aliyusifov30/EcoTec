using Microsoft.AspNetCore.Mvc;

namespace EcoTech.MVC.Controllers
{
	public class ServiceController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
