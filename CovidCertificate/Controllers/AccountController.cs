using CovidCertificate.Data;
using CovidCertificate.Data.Models;
using CovidCertificate.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCertificate.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole<string>> roleManager;
        private readonly SignInManager<User> signInManager;
        public AccountController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<string>> roleManager, SignInManager<User> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> CreateRole()
        {
            var result = await roleManager.CreateAsync(new IdentityRole<string>("User"));
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Register()
        {
            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            School school = context.School.FirstOrDefault(x => x.CodeByAdmin == model.AdminCode);
            if (school == null || !school.IsConfirmed)
            {
                return this.RedirectToAction("SchoolNotFound", "Account");
            }
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                SchoolId = school.Id
            };
            var result = await this.userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (model.Role.ToLower() == "teacher")
                {
                    var roleResult = await this.signInManager.UserManager.AddToRoleAsync(user, "Teacher");
                    if (roleResult.Errors.Any())
                    {
                        return this.RedirectToAction("Index", "Home");
                    }
                }
                else if (model.Role.ToLower() == "student")
                {
                    var roleResult = await this.signInManager.UserManager.AddToRoleAsync(user, "Student");
                    if (roleResult.Errors.Any())
                    {
                        return this.RedirectToAction("Index", "Home");
                    }
                }

            }
            else
            {
                return this.View(model);
            }
            await this.signInManager.SignInAsync(user, isPersistent: false);
            return this.RedirectToAction("Index", "Home");
        }
        public IActionResult SchoolNotFound()
        {
            return this.View();
        }
        public IActionResult Login()
        {
            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await this.signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }
        public IActionResult Logout()
        {
            this.signInManager.SignOutAsync();
            return this.RedirectToAction("Index", "Home");
        }
    }
}
