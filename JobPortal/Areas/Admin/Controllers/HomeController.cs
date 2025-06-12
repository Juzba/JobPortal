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
    public class HomeController(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) : Controller
    {
        private readonly ApplicationDbContext _db = db;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        public async Task<IActionResult> AdminPage()
        {
            List<UserModel> userList = [];
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

                foreach (var userInRole in usersInRole)
                {
                 var userMatch =  userList.FirstOrDefault(p => p.UserName == userInRole.UserName);
                    if (userMatch != null)
                    {
                        userMatch.Roles ??= [];
                        userMatch.Roles.Add(role);
                    }
                }
            }








            return View(userList);
        }
    }
}
