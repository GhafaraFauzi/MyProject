using Client.Models;
using Client.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using NuGet.Protocol;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        // Get Action
        public IActionResult Login()
        {
            //if (HttpContext.Session.GetString("email") == null)
            //{
                return View();
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Departemens");
            //}
        }

        //Post Action
        //[HttpPost]
        //public IActionResult Login(string Email)
        //{
        //    if (HttpContext.Session.GetString("email") == null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            HttpContext.Session.SetString("email", Email.ToString());
        //            return RedirectToAction("Index", "Departemens");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //    return View();
        //}

        //[Authentication]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public ActionResult Logout()
        //{
        //    HttpContext.Session.Clear();
        //    HttpContext.Session.Remove("email");
        //    return RedirectToAction("Login");
        //}
    }
}