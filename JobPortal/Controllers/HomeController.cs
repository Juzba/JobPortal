using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JobPortal.Controllers
{
    public class HomeController(ILogger<HomeController> logger, ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly ApplicationDbContext _db = db;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly UserManager<AppUser> _userManager = userManager;

        public IActionResult Index() => View();

        public async Task<IActionResult> JobList() => View(await _db.Jobs.ToListAsync());






        /////////// DETAILS //////////////////
        
        static Job? _DetailsJobModel;

        public async Task<IActionResult> Detail(int id)
        {
            _DetailsJobModel = await _db.Jobs.FindAsync(id);
            return View(_DetailsJobModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Detail(string text, int id)
        {
            if (text == null)
            {
                ViewBag.Error = "Zpráva musí obsahovat nějaký text!";
                return View(_DetailsJobModel);
            }

            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

            var message = new Message()
            {
                Text = text,
                DateTime = DateTime.Now,
                UserId = await _userManager.GetUserIdAsync(user),
            };

            await _db.Messages.AddAsync(message);
            await _db.SaveChangesAsync();

            ViewBag.Error = "Zpráva Odeslána!!";
            return View(_DetailsJobModel); ;
        }

           

        /////////// ERROR //////////////////

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
