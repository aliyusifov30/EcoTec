using Business.DTOs;
using Business.Helpers;
using Business.Repositories;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Drawing.Printing;

namespace EcoTech.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin")]

    public class SliderController : Controller
    {

        readonly ISliderRepository _sliderRepository;
        readonly FileManager _file;
        public SliderController(ISliderRepository sliderRepository, FileManager file)
        {
            _sliderRepository = sliderRepository;
            _file = file;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _sliderRepository.GetAll().Take(8);

            var asd = _sliderRepository.GetAll().ToList();

            int pageSize =8;


            ViewBag.PageSize = pageSize;
            TempData["Page"] = page;

            var a = PagenatedList<Slider>.Save(query, page, pageSize);

            return View(a);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slider entity)
        {

            if(!ModelState.IsValid)
            {
                return View();
            }

            if(entity.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image have to fill");
                return View(entity);
            }

            var list = await _file.UploadAsync("/uploads/sliders",entity.ImageFile);
            entity.Image = list.fileName;

            await _sliderRepository.AddAsync(entity);
            await _sliderRepository.SaveAsync();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _sliderRepository.GetByIdAsync(id,false);

            return View(entity);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Slider entity)
        {

            if (!ModelState.IsValid) { return View(entity); }

            var existEntity = await _sliderRepository.GetByIdAsync(entity.Id);

            if (existEntity == null) return RedirectToAction("error", "notfound");

            if (entity.ImageFile != null)
            {
                var list = await _file.UploadAsync("/uploads/sliders", entity.ImageFile);
                await _file.DeleteAsync("/uploads/sliders/", existEntity.Image);
                existEntity.Image = list.fileName;
            }

            existEntity.Title = entity.Title;
            existEntity.ButtonText1 = entity.ButtonText1;
            existEntity.ButtonText2 = entity.ButtonText2;
            existEntity.Description = entity.Description;
            await _sliderRepository.SaveAsync();

            int page = (int)TempData["Page"];

            return RedirectToAction("index", new { Page = page });
        }


        public async Task<IActionResult> Delete(int id)
        {

            int page = (int)TempData["Page"];

            bool check = await _sliderRepository.Remove(id);
            if (!check)
            {
                TempData["Error"] = "This data didn't removed";
                return RedirectToAction("index", new { Page = page });
            }

            var existEntity = await _sliderRepository.GetByIdAsync(id);
            await _file.DeleteAsync("/uploads/sliders/", existEntity.Image);


            await _sliderRepository.SaveAsync();

            return RedirectToAction("index", new { Page = page });
        }


    }
}
