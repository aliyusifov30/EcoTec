using Business.DTOs;
using Business.Helpers;
using Business.Repositories;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EcoTech.MVC.Areas.Manage.Controllers
{
	public class SocialMediaController : Controller
	{
		readonly ISocialMediaRepository _scMediaRepository;
		readonly FileManager _file;
		public SocialMediaController(ISocialMediaRepository scMediaRepository, FileManager file)
		{
			_scMediaRepository = scMediaRepository;
			_file = file;
		}

		public async Task<IActionResult> Index(int page = 1)
		{
			var query = _scMediaRepository.GetAll().Take(8);

			int pageSize = 8;

			ViewBag.PageSize = pageSize;
			TempData["Page"] = page;

			var a = PagenatedList<SocialMedia>.Save(query, page, pageSize);

			return View(a);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(SocialMedia entity)
		{

			if (!ModelState.IsValid)
			{
				return View();
			}

			await _scMediaRepository.AddAsync(entity);
			await _scMediaRepository.SaveAsync();

			return RedirectToAction("index");
		}

		public async Task<IActionResult> Edit(int id)
		{
			var entity = await _scMediaRepository.GetByIdAsync(id, false);

			return View(entity);
		}


		[HttpPost]
		public async Task<IActionResult> Edit(SocialMedia entity)
		{

			if (!ModelState.IsValid) { return View(entity); }

			var existEntity = await _scMediaRepository.GetByIdAsync(entity.Id);

			if (existEntity == null) return RedirectToAction("error", "notfound");

			existEntity.Link = entity.Link;
			existEntity.Icon = entity.Icon;

			await _scMediaRepository.SaveAsync();

			int page = (int)TempData["Page"];

			return RedirectToAction("index", new { Page = page });
		}


		public async Task<IActionResult> Delete(int id)
		{

			int page = (int)TempData["Page"];

			bool check = await _scMediaRepository.Remove(id);
			if (!check)
			{
				TempData["Error"] = "This data didn't removed";
				return RedirectToAction("index", new { Page = page });
			}

			await _scMediaRepository.SaveAsync();

			return RedirectToAction("index", new { Page = page });
		}
	}
}
