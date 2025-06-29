﻿using JobPortal.Code;
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
    public class HomeController(ApplicationDbContext db, UserManager<AppUser> userManager, Components components) : Controller
    {

        private readonly ApplicationDbContext _db = db;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly Components _components = components;


        ///////////// Employer Page ////////////////////////
        public async Task<IActionResult> EmployerPage()
        {
            if (User.IsInRole("Admin")) return View(await _db.Jobs.Include(p => p.Messages).Include(p => p.Employer).ToListAsync());
            return View(await _db.Jobs.Include(p => p.Messages).Where(p => p.Employer.UserName == User.Identity!.Name).ToListAsync());
        }


        ///////////// DETAILS ////////////////////////

        public async Task<IActionResult> DetailsJob(int id)
        {
            return View(await _db.Jobs.Include(p => p.Messages).ThenInclude(p => p.User).FirstOrDefaultAsync(p => p.Id == id));
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
            AppUser? user;

            if (job.EmployerId == null)
                user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            else
                user = await _userManager.FindByIdAsync(job.EmployerId);


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
