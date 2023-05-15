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
        readonly IWebHostEnvironment _env;
        public ServiceController(IServiceRepository serviceRepository, FileManager file, IWebHostEnvironment env)
        {
            _serviceRepository = serviceRepository;
            _file = file;
            _env = env;
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

            existEntity.Title = entity.Title;
            existEntity.Description = entity.Description;
            existEntity.Icon = entity.Icon;
            existEntity.Content = entity.Content;

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

            await _serviceRepository.SaveAsync();

            return RedirectToAction("index", new { Page = page });
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(List<IFormFile> imageFiles)
        {
            var file = Request.Form.Files.First();

            var item = await _file.UploadAsync(_env.WebRootPath + "/uploads/services/", file);

            var filePath = "";

            if (_env.IsDevelopment())
            {
                filePath = "https://localhost:7105/" + "/uploads/services/" + item.fileName;
            }
            else
            {
                filePath = "http://aliyusifov.com/" + "/uploads/services/" + item.fileName;
            }

            //if (System.IO.File.Exists((@"file.txt")))
            //{
            //    string existFile = System.IO.File.ReadAllText(@"file.txt");
            //    existFile = item.fileName + "|||" + existFile;
            //    System.IO.File.WriteAllText(@"file.txt", existFile);
            //}

            return Json(new { url = filePath });
        }

    }
}
