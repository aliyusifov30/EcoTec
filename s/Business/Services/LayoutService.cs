using Business.Repositories;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.OpenPgp;
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
		ISocialMediaRepository _socialMediaRepository;
        public LayoutService(UserManager<AppUser> user, IContactUsRepository contactUsRepository, ISocialMediaRepository socialMediaRepository)
        {
            _user = user;
            _contactUsRepository = contactUsRepository;
            _socialMediaRepository = socialMediaRepository;
        }

        public async Task<List<ContactUs>> GetContactUs() {
			return await _contactUsRepository.GetAll().ToListAsync();
		}

		public async Task<AppUser> GetAppsAsync(string userName)
		{
			return await _user.Users.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(userName));
		}

        public async Task<List<SocialMedia>> GetMediasAsync()
        {
            return await _socialMediaRepository.GetAll(false).ToListAsync();
        }

    }
}
