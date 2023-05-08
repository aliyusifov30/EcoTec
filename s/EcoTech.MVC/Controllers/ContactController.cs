using Business.Repositories;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EcoTech.MVC.Controllers
{
    public class ContactController : Controller
    {
        IContactUsRepository _contactUsRepository;

		public ContactController(IContactUsRepository contactUsRepository)
		{
			_contactUsRepository = contactUsRepository;
		}

		public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactUs contactUs) {
            if (!ModelState.IsValid) return View();
            contactUs.Status = false;
            var data=await _contactUsRepository.AddAsync(contactUs);

            await _contactUsRepository.SaveAsync();
            return View();
        }
    }
}
