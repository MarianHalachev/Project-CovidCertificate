using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CovidCertificate.Data;
using CovidCertificate.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CovidCertificate.ViewModels.Certificate;
using CovidCertificate.Services.Interfaces;

namespace CovidCertificate.Controllers
{
    public class CertificatesController : Controller
    {
        private readonly ICovidService covidService;
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext _context;

        public CertificatesController(ICovidService covidService, UserManager<User> userManager, ApplicationDbContext context)
        {
            this.covidService = covidService;
            this.userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> NotAvailableList()
        {
            User user = await userManager.GetUserAsync(this.User);
            NotAvailableCertificatesViewModel model = new NotAvailableCertificatesViewModel();
            model.Count = _context.Certificate.Where(x => x.DateOfIssue.AddMonths(x.ValidMonths) < DateTime.Now).Count();
            model.Certificates = _context.Certificate
                .Where(x => x.User.School.CodeByAdmin == user.School.CodeByAdmin && x.DateOfIssue.AddMonths(x.ValidMonths) < DateTime.Now)
                .Select(x => new CertificateViewModel()
                {
                    FullName = x.User.FirstName + x.User.LastName,
                    StartDate = x.DateOfIssue.ToShortDateString(),
                    EndDate = x.DateOfIssue.AddMonths(x.ValidMonths).ToShortDateString()
                })
                .ToList();
            return this.View(model);
        }


        public IActionResult NotAvailable()
        {
            return this.View();
        }

        public IActionResult Available()
        {
            return this.View();
        }


        public async Task<IActionResult> Index()
        {
            User user = await userManager.GetUserAsync(this.User);
            return View(await _context.Certificate.Where(x=>x.UserId==user.Id).ToListAsync());
        }


        [Authorize(Roles = "Admin,Student,Teacher")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Student,Teacher")]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            User applicationUser = await userManager.GetUserAsync(User);

            this.covidService.CreateCertificate(model.DateOfIssue,model.ValidMonths, applicationUser.Id);
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin,Student,Teacher")]
        public IActionResult Details(int id)
        {
            var certificate = this.covidService.GetCertificateById(id);
            var model = new DetailsViewModel()
            {
                Id = certificate.Id,
                DateOfIssue = certificate.DateOfIssue,
                ValidMonths = certificate.ValidMonths,
                User = certificate.User
            };

            return this.View(model);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var certificate = this.covidService.GetCertificateById(id);
            var model = new DetailsViewModel()
            {
                Id = certificate.Id,
                DateOfIssue = certificate.DateOfIssue,
                ValidMonths = certificate.ValidMonths,
                User = certificate.User
            };
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(DetailsViewModel model)
        {
            this.covidService.EditCertificate(model.Id, model.DateOfIssue,  model.ValidMonths);
            return this.RedirectToAction("Index", "Home");
        }



        [Authorize(Roles = "Admin")]
        // GET: Certificates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificate == null)
            {
                return NotFound();
            }

            return View(certificate);
        }

        // POST: Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certificate = await _context.Certificate.FindAsync(id);
            _context.Certificate.Remove(certificate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificateExists(int id)
        {
            return _context.Certificate.Any(e => e.Id == id);
        }


    }
}