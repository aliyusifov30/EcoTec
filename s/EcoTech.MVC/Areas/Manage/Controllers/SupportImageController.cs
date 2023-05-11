using Business.DTOs;
using Business.Helpers;
using Business.Repositories;
using Core.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EcoTech.MVC.Areas.Manage.Controllers
{
        [Area("Manage")]
    public class SupportImageController : Controller
    {
        readonly ISupportImageRepository _supportImageRepository;
        readonly FileManager _file;
        public SupportImageController(ISupportImageRepository supportImageRepository, FileManager file)
        {
            _supportImageRepository = supportImageRepository;
            _file = file;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _supportImageRepository.GetAll().Take(8);

            int pageSize = 8;

            ViewBag.PageSize = pageSize;
            TempData["Page"] = page;

            var a = PagenatedList<SupportImage>.Save(query, page, pageSize);

            return View(a);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupportImage entity)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (entity.ImageFileLeftTop == null)
            {
                ModelState.AddModelError("ImageFile", "Image have to fill");
                return View(entity);
            }
            if (entity.ImageFileRightTop == null)
            {
                ModelState.AddModelError("ImageFile", "Image have to fill");
                return View(entity);
            }
            if (entity.ImageFileLeftBottom == null)
            {
                ModelState.AddModelError("ImageFile", "Image have to fill");
                return View(entity);
            }
            if (entity.ImageFileRightBottom == null)
            {
                ModelState.AddModelError("ImageFile", "Image have to fill");
                return View(entity);
            }

            var list = await _file.UploadAsync("/uploads/supportImages", entity.ImageFileLeftTop);
            entity.ImageLeftTop = list.fileName;
            list= await _file.UploadAsync("/uploads/supportImages", entity.ImageFileRightTop);
            entity.ImageRightTop = list.fileName;
            list = await _file.UploadAsync("/uploads/supportImages", entity.ImageFileLeftBottom);
            entity.ImageLeftBottom = list.fileName;
            list = await _file.UploadAsync("/uploads/supportImages", entity.ImageFileRightBottom);
            entity.ImageRightBottom = list.fileName;



            await _supportImageRepository.AddAsync(entity);
            await _supportImageRepository.SaveAsync();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _supportImageRepository.GetByIdAsync(id, false);

            return View(entity);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(SupportImage entity)
        {
            if (!ModelState.IsValid) { return View(entity); }

            var existEntity = await _supportImageRepository.GetByIdAsync(entity.Id);

            if (existEntity == null) return RedirectToAction("error", "notfound");

            existEntity.Text = entity.Text;

            if (entity.ImageFileLeftTop != null)
            {
                var list = await _file.UploadAsync("/uploads/supportImages", entity.ImageFileLeftTop);
                await _file.DeleteAsync("/uploads/supportImages/", existEntity.ImageLeftTop);
                existEntity.ImageLeftTop = list.fileName;
            }
            if (entity.ImageFileRightTop != null)
            {
                var list = await _file.UploadAsync("/uploads/supportImages", entity.ImageFileRightTop);
                await _file.DeleteAsync("/uploads/supportImages/", existEntity.ImageRightTop);
                existEntity.ImageRightTop = list.fileName;
            }
            if (entity.ImageFileLeftBottom != null)
            {
                var list = await _file.UploadAsync("/uploads/supportImages", entity.ImageFileLeftBottom);
                await _file.DeleteAsync("/uploads/supportImages/", existEntity.ImageLeftBottom);
                existEntity.ImageLeftBottom = list.fileName;
            }
            if (entity.ImageFileRightBottom != null)
            {
                var list = await _file.UploadAsync("/uploads/supportImages", entity.ImageFileRightBottom);
                await _file.DeleteAsync("/uploads/supportImages/", existEntity.ImageRightBottom);
                existEntity.ImageRightBottom = list.fileName;
            }



            await _supportImageRepository.SaveAsync();

            int page = (int)TempData["Page"];

            return RedirectToAction("index", new { Page = page });
        }


        public async Task<IActionResult> Delete(int id)
        {

            int page = (int)TempData["Page"];

            bool check = await _supportImageRepository.Remove(id);
            if (!check)
            {
                TempData["Error"] = "This data didn't removed";
                return RedirectToAction("index", new { Page = page });
            }
			var existEntity = await _supportImageRepository.GetByIdAsync(id);
            await _file.DeleteAsync("/uploads/supportImages/", existEntity.ImageLeftTop);
            await _file.DeleteAsync("/uploads/supportImages/", existEntity.ImageRightTop);
            await _file.DeleteAsync("/uploads/supportImages/", existEntity.ImageLeftBottom);
            await _file.DeleteAsync("/uploads/supportImages/", existEntity.ImageRightBottom);

            await _supportImageRepository.SaveAsync();

            return RedirectToAction("index", new { Page = page });
        }

       
    }
}
