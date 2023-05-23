using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class DepartemenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
