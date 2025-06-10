using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JobPortal.Controllers
{
    public class HomeController(ILogger<HomeController> logger, ApplicationDbContext db) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly ApplicationDbContext _db = db;

        public IActionResult Index() => View();
        public async Task<IActionResult> Detail(int id) => View(await _db.Jobs.FindAsync(id));

        public async Task<IActionResult> JobList() => View(await _db.Jobs.ToListAsync());

        [Authorize]
        public IActionResult Buttons() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Buttons(int number)
        {
            Console.WriteLine("Cislo je:" + number);
            if (number == 1 && await _db.Users.FirstOrDefaultAsync(p => p.Email == "Katka@gmail.com") == null)
            {

                await _db.Users.AddAsync(new IdentityUser()
                {
                    Email = "Katka@gmail.com",
                    UserName = "Katka",
                    PasswordHash = "123456",
                    EmailConfirmed = true
                });
                await _db.SaveChangesAsync();

                Console.WriteLine("Pridán uživatel Katka");
            }

            return View();
        }



















        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
