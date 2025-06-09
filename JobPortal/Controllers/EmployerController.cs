using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Controllers
{
    [Authorize]
    public class EmployerController(ApplicationDbContext db, UserManager<IdentityUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _db = db;
        private readonly UserManager<IdentityUser> _userManager = userManager;

        public IActionResult AddJob() => View(new Job());


        [HttpPost]
        public async Task<IActionResult> AddJob(Job job)
        {
            var user = await _db.Users.FirstOrDefaultAsync(p => p.Email == User.Identity!.Name);
            if (user != null) {
                job.Employer = user;
                ModelState.Clear();
            };
            

            if (!ModelState.IsValid) return View(job);

            await _db.AddAsync(job);
            await _db.SaveChangesAsync();

            return RedirectToAction("JobList", "Home");
        }
    }
}
