using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CovidCertificate.Data;
using CovidCertificate.Data.Models;
using CovidCertificate.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CovidCertificate.ViewModels.School;

namespace CovidCertificate.Controllers
{
    public class SchoolsController : Controller
    {
        private readonly ICovidService covidService;
        private readonly UserManager<User> userManager;

        private readonly ApplicationDbContext _context;



        public SchoolsController(ICovidService covidService, UserManager<User> userManager,
            ApplicationDbContext context)
        {
            this.covidService = covidService;
            this.userManager = userManager;
            _context = context;
        }


      
        public async Task<IActionResult> Index()
        {
            var users = _context.Users
                .Join(_context.Certificate, u => u.Id, c => c.UserId, (user, certificate) => user)
                .ToList();
            return View(await _context.School.ToListAsync());
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CreateViewModel model)
        {
            this.covidService.CreateSchool(model.Name,model.CodeByAdmin,model.Address);
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Details(int id)
        {
            var school = this.covidService.GetSchoolById(id);
            var model = new DetailsViewModel()
            {
             Id=school.Id,
             CodeByAdmin = school.CodeByAdmin,
             Name = school.Name,
             Address =school.Address
            };

            return this.View(model);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var school = this.covidService.GetSchoolById(id);
            var model = new DetailsViewModel()
            {
                Id = school.Id,
                CodeByAdmin = school.CodeByAdmin,
                Name = school.Name,
                Address = school.Address
            };
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(DetailsViewModel model)
        {
            this.covidService.EditSchool(model.Id,model.Name,model.CodeByAdmin,model.Address);
            return this.RedirectToAction("Index", "Home");
        }

        // GET: Schools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await _context.School
                .FirstOrDefaultAsync(m => m.Id == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var school = await _context.School.FindAsync(id);
            _context.School.Remove(school);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolExists(int id)
        {
            return _context.School.Any(e => e.Id == id);
        }
    }
}
