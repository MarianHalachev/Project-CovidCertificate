using CovidCertificate.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CovidCertificate.Controllers
{
    public class SeedController : Controller
    {
        private readonly IDataSeeder seeder;

        public SeedController(IDataSeeder seeder)
        {
            this.seeder = seeder;
        }
        public async Task<IActionResult> Index()
        {
           await seeder.Run();
            return Json("You seed data...");
        }
    }
}
