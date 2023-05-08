using Business.DTOs;
using Business.Repositories;
using Business.Services.Abstractions;
using Core.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoTech.MVC.Areas.Manage.Controllers
{
	[Area("manage")]
	[Authorize(Roles ="SuperAdmin")]
	public class ContactUsController : Controller
	{
		IContactUsRepository _contactUsRepository;
        IEmailService _emailService;
        public ContactUsController(IContactUsRepository contactUsRepository, IEmailService emailService)
        {
            _contactUsRepository = contactUsRepository;
            _emailService = emailService;
        }

        public IActionResult Index(int page=1)
		{
            var query = _contactUsRepository.GetAll().Take(8);

            int pageSize = 8;

            ViewBag.PageSize = pageSize;
            TempData["Page"] = page;

            var a = PagenatedList<ContactUs>.Save(query, page, pageSize);
            return View(a);
		}
        public async Task<IActionResult> Message(int id)
        {
            var message = await _contactUsRepository.GetByIdAsync(id);

            if (message == null) return RedirectToAction("notfound", "error");

            return View(message);
        }

        public async Task<IActionResult> AcceptMessage(ContactUs contactUs)
        {
            ContactUs contactExist =await _contactUsRepository.GetByIdAsync(contactUs.Id);

            if (contactExist == null) return RedirectToAction("notfound", "Error");


            _emailService.SendEmail(contactExist.Email, $"Ecotec.", $"Salam {contactExist.FullName}.\n {contactUs.Text}");
            contactExist.Status = true;
            await _contactUsRepository.SaveAsync();
            return RedirectToAction("index", "contactus");
        }

        public async Task<IActionResult> Delete(int id)
        {
            int page = (int)TempData["Page"];            
             bool result=await _contactUsRepository.Remove(id);
            if (!result) {
                TempData["Error"] = "This data didn't removed";
                return RedirectToAction("index", new { Page = page });
            }
            await _contactUsRepository.SaveAsync();
            return RedirectToAction("index", new { Page = page });
        }
        
    }
}
