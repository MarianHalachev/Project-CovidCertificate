using CovidCertificate.Data;
using CovidCertificate.Data.Models;
using CovidCertificate.ViewModels.SchoolAdmins;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCertificate.Controllers
{
    public class SchoolAdminsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SchoolAdminsController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            SchoolAdminIndexViewModel model = new SchoolAdminIndexViewModel();
            User user = await userManager.GetUserAsync(this.User);
            int schoolId = context.School.Where(x => x.Id == user.SchoolId).FirstOrDefault().Id;
            model.TotalTestsNextWeek = context.Users

                .Where(x => x.School.Id == schoolId).ToList()
                .Where(x => !x.Certificate.Any(c => c.DateOfIssue.AddDays(7 - (int)DateTime.Now.DayOfWeek) < DateTime.Now)).Count();

            model.Teachers = context.Users
                .Where(x => x.School.Id == schoolId)
                .Select(x => new UserViewModel()
                {
                    FullName = x.FirstName + " " + x.LastName,
                    HasValidCertificate = x.Certificate.Any(c => c.DateOfIssue.AddMonths(c.ValidMonths) > DateTime.Now)
                })
                .ToList();

            model.Students = context.Users
                 .Where(x => x.School.Id == schoolId && (x.Roles.Any(r => r.Name == "student")))
                .Select(x => new UserViewModel()
                {
                    FullName = x.FirstName + " " + x.LastName,
                    HasValidCertificate = x.Certificate.Any(c => c.DateOfIssue.AddMonths(c.ValidMonths) > DateTime.Now)
                })
                .ToList();

            return View(model);
        }

        private bool CertificateIsValid(Certificate certificate)
        {
            return certificate.DateOfIssue.AddMonths(certificate.ValidMonths) > DateTime.Now;
        }
    }
}
