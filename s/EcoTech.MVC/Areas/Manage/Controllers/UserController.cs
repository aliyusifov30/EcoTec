using Business.DTOs;
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

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 8;

            var query = _userManager.Users.Select(user => new UserVM
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
            }).AsQueryable().Take(pageSize);


            ViewBag.PageSize = pageSize;
            TempData["Page"] = page;

            var a = PagenatedList<UserVM>.Save(query, page, pageSize);

            return View(a);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserVM userVM) {

            if (!ModelState.IsValid) return View();

            AppUser user = null;
            //todo regex
            
            user = await _userManager.Users.FirstOrDefaultAsync(x=>x.Email.ToLower().Equals(userVM.Email.ToLower()));
            
            if(user != null)
            {
                ModelState.AddModelError("Email", "Exist Email");
                return View(userVM);
            }
            user = new AppUser
            {
                UserName = userVM.UserName,
                Email = userVM.Email,
                FullName = userVM.FullName
            };



            var result =await _userManager.CreateAsync(user, userVM.Password);
            if (!result.Succeeded)
            {
                return View(userVM);
            }
            
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("index", "user");
        }
        public async Task<IActionResult> Edit(string id)
        {

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            var data = new UserVM
            {
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName,
                Id = id
            };

            return View(data);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(UserVM user)
        {
            var existUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existUser == null)
            {
                return RedirectToAction("notFound", "page");
            }

            var result = await _userManager.CheckPasswordAsync(existUser, user.Password);

            if (result == false)
            {
                ModelState.AddModelError("Password", "Old password wrong");
                return View(existUser);
            }

            await _userManager.ChangePasswordAsync(existUser, user.Password, user.NewPassword);

            existUser.UserName = user.UserName;
            existUser.Email = user.Email;
            existUser.FullName = user.FullName;
            
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("notfound", "error");
            }
            await _userManager.DeleteAsync(user);
            return RedirectToAction("index");

        }
    }
}
