
using Business.ViewModels;
using Core.Entities;
using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EcoTech.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {

        //UserName: SahilIbrahimov
        //Password: Admin.684a2
        //Role: SuperAdmin
        UserManager<AppUser> _usersManager;
        RoleManager<IdentityRole> _roleManager;
        SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> usersManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, DataContext _context)
        {
            _usersManager = usersManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Role()
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel loginVM) {
            if (!ModelState.IsValid) return View();
            if (!ModelState.IsValid) return View();

            AppUser admin = await _usersManager.FindByNameAsync(loginVM.UserName);

            if (admin == null)
            {
                ModelState.AddModelError("", "UserName or Password is not corrent");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(admin, loginVM.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is not corrent");
                return View();
            }

            return RedirectToAction("index", "dashboard");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            
            return RedirectToAction("login");
        }      
    }
}
