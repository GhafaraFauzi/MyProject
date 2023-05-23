using Client.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class DepartemensController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
