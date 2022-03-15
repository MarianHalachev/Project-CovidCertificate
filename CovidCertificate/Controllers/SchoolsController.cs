using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CovidCertificate.Data;
using CovidCertificate.Data.Models;
using CovidCertificate.ViewModels.School;
using Microsoft.AspNetCore.Identity;

namespace CovidCertificate.Controllers
{
    public class SchoolsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SchoolsController(ApplicationDbContext context, SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // GET: Schools
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.School.Include(s => s.Admin);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Schools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await context.School
                .Include(s => s.Admin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        // GET: Schools/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(context.Users, "Id", "Id");
            return View();
        }

        public IActionResult RegisterSchool()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterSchool(RegisterSchoolViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("RegisterSchool", "Schools");
            }
            School school = new School()
            {
                Name=model.SchoolName,
                Address = model.Address,
                CodeByAdmin = model.CodeByAdmin
            };
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                SchoolId = school.Id
            };
            school.Admin = user;
            var result = await this.userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                var roleResult = await this.signInManager.UserManager.AddToRoleAsync(user, "SchoolAdmin");
                if (roleResult.Errors.Any())
                {
                    return this.RedirectToAction("RegisterSchool","Schools");
                }
            }
            else
            {
                return this.View(model);
            }
            await this.signInManager.SignInAsync(user, isPersistent: false);
            //TODO: Redirect to school users list
            return this.RedirectToAction("Index", "Schools");

        }

        // POST: Schools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AdminId,CodeByAdmin,Address,IsConfirmed")] School school)
        {
            if (ModelState.IsValid)
            {
                context.Add(school);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(context.Users, "Id", "Id", school.AdminId);
            return View(school);
        }

        // GET: Schools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await context.School.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(context.Users, "Id", "Id", school.AdminId);
            return View(school);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AdminId,CodeByAdmin,Address,IsConfirmed")] School school)
        {
            if (id != school.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(school);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolExists(school.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(context.Users, "Id", "Id", school.AdminId);
            return View(school);
        }

        // GET: Schools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await context.School
                .Include(s => s.Admin)
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
            var school = await context.School.FindAsync(id);
            context.School.Remove(school);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolExists(int id)
        {
            return context.School.Any(e => e.Id == id);
        }
    }
}
