using Business.Repositories;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class SettingService
	{
		ISettingService _settingService;


		public SettingService(ISettingService settingService)
		{
			_settingService = settingService;
		}

		public Dictionary<string,string> GetSettings()
		{
			return  _settingService.GetAll().ToDictionary(setting=>setting.Key,setting=>setting.Value);
		}

	}
}
