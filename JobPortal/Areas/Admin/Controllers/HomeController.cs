using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[area]/[controller]/{id?}")]
    public class HomeController : Controller
    {
        public IActionResult AdminPage()
        {
            return View();
        }
    }
}
