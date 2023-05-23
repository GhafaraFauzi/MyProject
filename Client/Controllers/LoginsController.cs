using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class LoginsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}





//[Route("api/[controller]")]
//[ApiController]
//public class LoginsController : ControllerBase
//{
//    private IConfiguration _config;
//    public LoginsController(IConfiguration config)
//    {
//        _config = config;
//    }

//    [AllowAnonymous]
//    [HttpPost]
//    public IActionResult Login([FromBody] Account account)
//    {
//        var user = Authenticate(account);
//        if (user != null)
//        {
//            var token = Generate(account);
//            return Ok(token);
//        }
//        return NotFound("User Tidak Ditemukan");
//    }

//    private string Generate(Employee account)
//    {
//        throw new NotImplementedException();
//    }

//    private Employee Authenticate(Account account)
//    {
//        throw new NotImplementedException();
//    }
//}