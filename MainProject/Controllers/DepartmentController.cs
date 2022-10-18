using Microsoft.AspNetCore.Mvc;

namespace MainProject.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
