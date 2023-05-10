using Business.DTOs;
using Business.Helpers;
using Business.Repositories;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoTech.MVC.Areas.Manage.Controllers
{
	[Area("Manage")]
	[Authorize(Roles ="SuperAdmin")]
	public class CompanyImageController : Controller
	{
		readonly ICompanyImageRepository _companyImageRepository;
		readonly FileManager _file;
		public CompanyImageController(ICompanyImageRepository companyImageRepository, FileManager file)
		{
			_companyImageRepository = companyImageRepository;
			_file = file;
		}

		public IActionResult Index(int page = 1)
		{
			var query = _companyImageRepository.GetAll().Take(8);

			int pageSize = 8;

			ViewBag.PageSize = pageSize;
			TempData["Page"] = page;

			var a = PagenatedList<CompanyImage>.Save(query, page, pageSize);

			return View(a);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CompanyImage entity)
		{

			if (entity.ImageFile == null)
			{
				ModelState.AddModelError("ImageFile", "Image have to fill");
				return View(entity);
			}

			var list = await _file.UploadAsync("/uploads/companyImages/", entity.ImageFile);
			entity.Image = list.fileName;

			await _companyImageRepository.AddAsync(entity);
			await _companyImageRepository.SaveAsync();

			return RedirectToAction("index");
		}

		public async Task<IActionResult> Edit(int id)
		{
			var entity = await _companyImageRepository.GetByIdAsync(id, false);

			return View(entity);
		}


		[HttpPost]
		public async Task<IActionResult> Edit(CompanyImage entity)
		{
			var existEntity = await _companyImageRepository.GetByIdAsync(entity.Id);

			if (existEntity == null) return RedirectToAction("error", "notfound");

			if (entity.ImageFile != null)
			{
				var list = await _file.UploadAsync("/uploads/companyImages/", entity.ImageFile);
				await _file.DeleteAsync("/uploads/companyImages/", existEntity.Image);
				existEntity.Image = list.fileName;
			}

			await _companyImageRepository.SaveAsync();

			int page = (int)TempData["Page"];

			return RedirectToAction("index", new { Page = page });
		}


		public async Task<IActionResult> Delete(int id)
		{

			int page = (int)TempData["Page"];

			bool check = await _companyImageRepository.Remove(id);
			if (!check)
			{
				TempData["Error"] = "This data didn't removed";
				return RedirectToAction("index", new { Page = page });
			}

			var existEntity = await _companyImageRepository.GetByIdAsync(id);
			await _file.DeleteAsync("/uploads/companyImages/", existEntity.Image);


			await _companyImageRepository.SaveAsync();

			return RedirectToAction("index", new { Page = page });
		}
	}
}
