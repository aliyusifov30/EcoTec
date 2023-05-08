using Business.Repositories;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EcoTech.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin")]

    public class DashboardController : Controller
    {
        IContactUsRepository _contactUsRepository;

		public DashboardController(IContactUsRepository contactUsRepository)
		{
			_contactUsRepository = contactUsRepository;
		}

		public IActionResult Index()
        {
            List<ContactUs> contactMessages= _contactUsRepository.GetAll().ToList();
            return View(contactMessages);
        }
    }
}
