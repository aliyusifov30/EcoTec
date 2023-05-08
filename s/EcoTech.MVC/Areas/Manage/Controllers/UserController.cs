using Business.ViewModels;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoTech.MVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class UserController : Controller
    {
        UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var data = _userManager.Users.Select(user => new UserVM
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
            }).ToList();
            return View(data);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserVM userVM) {
            if (!ModelState.IsValid) return View();
            AppUser user = new AppUser 
            {
                Id=userVM.Id,
                UserName = userVM.UserName,
                Email=userVM.Email,
                FullName = userVM.FullName
            };
            var result=await _userManager.CreateAsync(user, userVM.Password);
            if (!result.Succeeded)
            {
                return View();
            }
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("index", "user");
        }
        public async Task<IActionResult> EditUser(string id)
        {

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            var data = new UserVM
            {
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName,
            };

            return View(data);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditUser(UserVM user)
        {
            var existUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
            existUser.UserName = user.UserName;
            existUser.Email = user.Email;
            existUser.FullName = user.FullName;
            if (!string.IsNullOrEmpty(user.Password))
            {
                await _userManager.ResetPasswordAsync(existUser, null,user.Password);
            }
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("notfound", "error");
            }
            await _userManager.UpdateAsync(user);
            return Ok();
        }
    }
}
