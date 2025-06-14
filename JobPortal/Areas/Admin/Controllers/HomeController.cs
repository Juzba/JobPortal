using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[area]/[controller]/{id?}")]
    public class HomeController(ApplicationDbContext db, UserManager<AppUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _db = db;
        private readonly UserManager<AppUser> _userManager = userManager;
        static List<UserModel> userList = [];

        public async Task<IActionResult> AdminPage()
        {
            string[] roles = ["Admin", "Employer"];

            userList = await _userManager.Users.Select(p => new UserModel()
            {
                UserName = p.UserName,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                IsEmailConfirmed = p.EmailConfirmed
            }).ToListAsync();


            foreach (var role in roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role);

                userList = userList.Select(p =>
                {
                    p.Roles ??= [];
                    if (usersInRole.Any(u => u.UserName == p.UserName))
                    {
                        p.Roles.Add(role);
                    }
                    return p;
                }).ToList();
            }

            return View(userList);
        }

        [HttpPost]
        public async Task<IActionResult> AdminPage(string role, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null || role == null) return View(userList);

            if (await _userManager.IsInRoleAsync(user, role)) 
            {
                if (role == "Admin" && userList.Where(p => p.Roles?.Contains("Admin") == true).Count() == 1) return View(userList);
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            else
                await _userManager.AddToRoleAsync(user, role);

            return RedirectToAction("AdminPage", "Home");
        }



    }
}
