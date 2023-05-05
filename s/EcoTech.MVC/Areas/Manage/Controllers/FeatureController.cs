using Business.DTOs;
using Business.Helpers;
using Business.Repositories;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EcoTech.MVC.Areas.Manage.Controllers
{
	[Area("Manage")]
    public class FeatureController : Controller
    {
		readonly IFeatureRepository _featuresRepository;
		readonly FileManager _file;
		public FeatureController(IFeatureRepository featuresRepository, FileManager file)
		{
			_featuresRepository = featuresRepository;
			_file = file;
		}

		public async Task<IActionResult> Index(int page = 1)
		{
			var query = _featuresRepository.GetAll().Take(8);

			int pageSize = 8;

			ViewBag.PageSize = pageSize;
			TempData["Page"] = page;

			var a = PagenatedList<Feature>.Save(query, page, pageSize);

			return View(a);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Feature entity)
		{

			if (!ModelState.IsValid)
			{
				return View();
			}

			await _featuresRepository.AddAsync(entity);
			await _featuresRepository.SaveAsync();

			return RedirectToAction("index");
		}

		public async Task<IActionResult> Edit(int id)
		{
			var entity = await _featuresRepository.GetByIdAsync(id, false);

			return View(entity);
		}


		[HttpPost]
		public async Task<IActionResult> Edit(Feature entity)
		{

			if (!ModelState.IsValid) { return View(entity); }

			var existEntity = await _featuresRepository.GetByIdAsync(entity.Id);

			if (existEntity == null) return RedirectToAction("error", "notfound");

			existEntity.Title = entity.Title;
			existEntity.Description = entity.Description;
			existEntity.Icon = entity.Icon;

			await _featuresRepository.SaveAsync();

			int page = (int)TempData["Page"];

			return RedirectToAction("index", new { Page = page });
		}


		public async Task<IActionResult> Delete(int id)
		{

			int page = (int)TempData["Page"];

			bool check = await _featuresRepository.Remove(id);
			if (!check)
			{
				TempData["Error"] = "This data didn't removed";
				return RedirectToAction("index", new { Page = page });
			}

			await _featuresRepository.SaveAsync();

			return RedirectToAction("index", new { Page = page });
		}
	}
}
