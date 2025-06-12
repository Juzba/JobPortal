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
    public class HomeController(ApplicationDbContext db, UserManager<IdentityUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _db = db;
        private readonly UserManager<IdentityUser> _userManager = userManager;
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
        public IActionResult AdminPage(int cislo)
        {
            Console.WriteLine("cus");
            return View(userList);
        }



    }
}
