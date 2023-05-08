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

    public class SettingController : Controller
	{
		readonly ISettingService _settingService;
		private readonly FileManager _fileService;
		private readonly IWebHostEnvironment _env;
		public SettingController(IWebHostEnvironment env, ISettingService settingService, FileManager fileService)
		{
			_env = env;
			_settingService = settingService;
			_fileService = fileService;
		}

		public async Task<IActionResult> Index(int page = 1)
		{
			var settings = _settingService.GetAll(false);
			ViewBag.PageSize = 8;
			return View(PagenatedList<Setting>.Save(settings, page, 8));
		}

		public async Task<IActionResult> Edit(int id)
		{
			var setting = await _settingService.GetAsync(x=>x.Id == id);
			return View(setting);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Setting setting)
		{
			//if (!ModelState.IsValid) return View(setting);

			var oldSetting = await _settingService.GetAsync(x => x.Id == setting.Id);

			if (setting.ImageFile != null)
			{
				await _fileService.DeleteAsync("/uploads/settings/", oldSetting.Value);
				var item = await _fileService.UploadAsync("/uploads/settings/",setting.ImageFile);
				oldSetting.Value = item.fileName;
            }
			else
			{
				oldSetting.Value = setting.Value;
			}

			await _settingService.SaveAsync();
			//todo commit 
			return RedirectToAction("index", "setting");
		}

	}
}
