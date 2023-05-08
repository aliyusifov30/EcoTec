using Business.DTOs;
using Business.Helpers;
using Business.Repositories;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EcoTech.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin")]

    public class ServiceController : Controller
    {
        readonly IServiceRepository _serviceRepository;
        readonly FileManager _file;
        public ServiceController(IServiceRepository serviceRepository, FileManager file)
        {
            _serviceRepository = serviceRepository;
            _file = file;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _serviceRepository.GetAll().Take(8);

            int pageSize = 8;

            ViewBag.PageSize = pageSize;
            TempData["Page"] = page;

            var a = PagenatedList<Service>.Save(query, page, pageSize);

            return View(a);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Service entity)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (entity.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image have to fill");
                return View(entity);
            }

            var list = await _file.UploadAsync("/uploads/services", entity.ImageFile);
            entity.Image = list.fileName;

            await _serviceRepository.AddAsync(entity);
            await _serviceRepository.SaveAsync();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _serviceRepository.GetByIdAsync(id, false);

            return View(entity);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Service entity)
        {

            if (!ModelState.IsValid) { return View(entity); }

            var existEntity = await _serviceRepository.GetByIdAsync(entity.Id);

            if (existEntity == null) return RedirectToAction("error", "notfound");

            if (entity.ImageFile != null)
            {
                var list = await _file.UploadAsync("/uploads/services", entity.ImageFile);
                await _file.DeleteAsync("/uploads/services/", existEntity.Image);
                existEntity.Image = list.fileName;
            }

            existEntity.Title = entity.Title;
            existEntity.Description = entity.Description;
            

            await _serviceRepository.SaveAsync();

            int page = (int)TempData["Page"];

            return RedirectToAction("index", new { Page = page });
        }


        public async Task<IActionResult> Delete(int id)
        {

            int page = (int)TempData["Page"];

            bool check = await _serviceRepository.Remove(id);
            if (!check)
            {
                TempData["Error"] = "This data didn't removed";
                return RedirectToAction("index", new { Page = page });
            }

            var existEntity = await _serviceRepository.GetByIdAsync(id);
            await _file.DeleteAsync("/uploads/services/", existEntity.Image);


            await _serviceRepository.SaveAsync();

            return RedirectToAction("index", new { Page = page });
        }

    }
}
