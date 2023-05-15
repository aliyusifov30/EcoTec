using Business.Repositories;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace EcoTech.MVC.Controllers
{
	public class ServiceController : Controller
	{
		IServiceRepository _serviceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IActionResult> Index(int id)
		{
			var ser = await _serviceRepository.GetByIdAsync(id);

			if (ser == null) return RedirectToAction("NotFound", "Pages");

			return View(ser);
		}
	}
}
