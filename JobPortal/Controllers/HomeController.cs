using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;

namespace JobPortal.Controllers
{
    public class HomeController(ILogger<HomeController> logger, ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly ApplicationDbContext _db = db;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly UserManager<AppUser> _userManager = userManager;

        public IActionResult Index() => View();


        /////////// JOBLIST //////////////////
        public async Task<IActionResult> JobList(int id)
        {
            FilterModel filterModel = new();

            if (id == 0)
                filterModel = TempData.Peek("filter") is string dataJson ? JsonSerializer.Deserialize<FilterModel>(dataJson) ?? new() : new();



            int itemsOnPageCount = 6;
            int pagesCount = filterModel.PageNumber * itemsOnPageCount;

            var filteredJobs = _db.Jobs
                .Where(p => p.Location.Contains(filterModel.Location ?? ""))
                .Where(p => p.Title.Contains(filterModel.JobPosition ?? ""))
                .Where(p => p.Salary >= filterModel.MinSalary && p.Salary <= filterModel.MaxSalary);

            var jobs = await filteredJobs
             .Skip(pagesCount)
             .Take(itemsOnPageCount)
             .ToListAsync();



            int lastPage = filteredJobs.Count() % itemsOnPageCount == 0 ? 0 : 1;
            ViewBag.PagesCount = filteredJobs.Count() / itemsOnPageCount + lastPage;

            ViewBag.PageNumber = filterModel.PageNumber;
            ViewBag.Count = filteredJobs.Count();
            return View(jobs);
        }

        [HttpPost]
        public IActionResult JobPage(int page)
        {
            FilterModel data = TempData["filter"] is string jsonData ? JsonSerializer.Deserialize<FilterModel>(jsonData) ?? new() : new();
            data.PageNumber = page;
            TempData["filter"] = JsonSerializer.Serialize(data);


            return RedirectToAction("JobList", "Home");
        }



        /////////// SEARCH FILTER //////////////////
        public IActionResult SearchFilter() => View(new FilterModel());
        [HttpPost]
        public IActionResult SearchFilter(FilterModel filterModel)
        {
            if (filterModel.MinSalary >= filterModel.MaxSalary) ModelState.AddModelError("", "Spodní hranice mzdy musí být menší než horní hranice.");
            if (!ModelState.IsValid) return View(filterModel);


            TempData["filter"] = JsonSerializer.Serialize(filterModel);
            return RedirectToAction("JobList", "Home");
        }




        /////////// DETAILS //////////////////

        public async Task<IActionResult> Detail(int id) => View(await _db.Jobs.FindAsync(id));


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Detail(string text, int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

            if (user != null && text != null)
            {

                var message = new Message()
                {
                    Text = text,
                    DateTime = DateTime.Now,
                    UserId = await _userManager.GetUserIdAsync(user),
                    JobId = id
                };

                await _db.Messages.AddAsync(message);
                await _db.SaveChangesAsync();

                TempData["info"] = "Zpráva Odeslána!!";
                return RedirectToAction("Detail");
            }
            TempData["info"] = text == null ? "Zpráva musí obsahovat nějaký text!" : "Uživatel nenalezen!";
            return RedirectToAction("Detail");
        }



        /////////// ERROR //////////////////

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
