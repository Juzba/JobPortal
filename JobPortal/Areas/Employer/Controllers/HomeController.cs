using JobPortal.Code;
using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Areas.Employer.Controllers
{
    [Area("Employer")]
    [Authorize(Roles = "Employer, Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class HomeController(ApplicationDbContext db, UserManager<IdentityUser> userManager, Components components) : Controller
    {

        private readonly ApplicationDbContext _db = db;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly Components _components = components;


        ///////////// Employer Page ////////////////////////
        public async Task<IActionResult> EmployerPage()
        {
            if (User.Identity == null) return View(new List<Job>());
            if (User.IsInRole("Admin")) return View(await _db.Jobs.ToListAsync());
            return View(await _db.Jobs.Where(p => p.Employer.UserName == User.Identity.Name).ToListAsync());
        }




        ///////////// ADD/EDIT JOB ////////////////////////
        public async Task<IActionResult> AddJob(int id)
        {
            if (id == 0) return View(_components.RandomJob());

            return View(await _db.Jobs.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddJob(Job job)
        {

            if (User.Identity?.Name == null) return View(job);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                job.Employer = user;
                job.DatePosted = DateTime.Now;
                ModelState.Clear();
            }



            if (!ModelState.IsValid) return View(job);

            if (job.Id == 0) await _db.AddAsync(job); // Create
            else _db.Update(job);                     //edit
            await _db.SaveChangesAsync();

            return RedirectToAction("EmployerPage");
        }


        ///////////// DELETE ////////////////////////
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _db.Jobs.FindAsync(id);
            if (user != null)
            {
                _db.Jobs.Remove(user);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("EmployerPage");
        }

    }

}
