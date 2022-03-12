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

        public CertificatesController(ICovidService covidService, UserManager<User> userManager,ApplicationDbContext context)
        {
            this.covidService = covidService;
            this.userManager = userManager;
            _context = context;
        }

        public IActionResult NotAvailable()
        {
            return this.View();
        }

        public IActionResult Available()
        {
            return this.View();
        }

        public IActionResult Check(DateTime endDate, int validMonths, int id) 
        {
            var certificate = this.covidService.GetCertificateById(id);
            var model = new DetailsViewModel()
            {
                Id = certificate.Id,
                EndDate = certificate.EndDate,
                ValidMonths = certificate.ValidMonths,
                User = certificate.User
            };
            if (certificate.EndDate>DateTime.UtcNow)
            {

                return RedirectToAction("Available", "Certificates");
            }
            else
            {
                return RedirectToAction("NotAvailable", "Certificates");
            }      
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Certificate.ToListAsync());
        }


        [Authorize(Roles = "Admin,User")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create(CreateViewModel model)
        {
            this.covidService.CreateCertificate(model.EndDate, model.ValidMonths);
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Details(int id)
        {
            var certificate = this.covidService.GetCertificateById(id);
            var model = new DetailsViewModel()
            {
                Id = certificate.Id,
                EndDate = certificate.EndDate,
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
                EndDate = certificate.EndDate,
                ValidMonths = certificate.ValidMonths,
                User = certificate.User
            };
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(DetailsViewModel model)
        {
            this.covidService.EditCertificate(model.Id, model.EndDate, model.ValidMonths);
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