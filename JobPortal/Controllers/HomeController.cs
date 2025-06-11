using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JobPortal.Controllers
{
    public class HomeController(ILogger<HomeController> logger, ApplicationDbContext db, RoleManager<IdentityRole> roleManager) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly ApplicationDbContext _db = db;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public IActionResult Index() => View();
        public async Task<IActionResult> Detail(int id) => View(await _db.Jobs.FindAsync(id));

        public async Task<IActionResult> JobList() => View(await _db.Jobs.ToListAsync());

      
        public IActionResult Buttons() => View();

       
        [HttpPost]
        public async Task<IActionResult> Buttons(int number)
        {
            Console.WriteLine("Cislo je:" + number);
            if (number == 1)
            {
                var user = new IdentityUser
                {
                    Id = "user1-id-2478652fdsss154",
                    UserName = "Juzba88@gmail.com",
                    NormalizedUserName = "JUZBA88@GMAIL.COM",
                    Email = "Juzba88@gmail.com",
                    NormalizedEmail = "JUZBA88@GMAIL.COM",
                    EmailConfirmed = true,
                    SecurityStamp = "security-stamp-1-14fds-fsd14dsf-dsfsdf5",
                    ConcurrencyStamp = "concurency-stamp-1-112dsd-fsdsffsf-1444",
                };

                var hasher = new PasswordHasher<IdentityUser>();
                user.PasswordHash = hasher.HashPassword(user, "123456");
                Console.WriteLine(user.PasswordHash);
            }

            if (number == 2)
            {
                var adminRoleName = "admin";

                await _roleManager.CreateAsync(new IdentityRole(adminRoleName));

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
