using Business.Repositories;
using Core.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EcoTech.MVC.Controllers
{
    public class ContactController : Controller
    {
        IContactUsRepository _contactUsRepository;
		ISettingService _settingServiceRepository;
		public ContactController(IContactUsRepository contactUsRepository,ISettingService settingService)
		{
			_contactUsRepository = contactUsRepository;
            _settingServiceRepository = settingService;
		}

		public IActionResult Index()
        {
			ViewData["settings"] = _settingServiceRepository.GetAll().ToDictionary(x => x.Key, x => x.Value);

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
