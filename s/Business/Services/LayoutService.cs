using Business.Repositories;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public class LayoutService
	{
		UserManager<AppUser> _user;
		IContactUsRepository _contactUsRepository;

		public LayoutService(UserManager<AppUser> user, IContactUsRepository contactUsRepository)
		{
			_user = user;
			_contactUsRepository = contactUsRepository;
		}

		public async Task<List<ContactUs>> GetContactUs() {
			return await _contactUsRepository.GetAll().ToListAsync();
		}
	}
}
