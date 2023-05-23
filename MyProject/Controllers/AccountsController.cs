using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyProject.Context;
using MyProject.Models;
using MyProject.Repository;
using MyProject.Repository.Interface;
using MyProject.ViewModels;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountRepository accountRepository;
        private readonly EmployeeRepository employeeRepository;
        private readonly IConfiguration _configuration;
        public static Employee employee = new Employee();

        public AccountController(AccountRepository accountRepository, EmployeeRepository employeeRepository, IConfiguration configuration)
        {
            this.accountRepository = accountRepository;
            this.employeeRepository = employeeRepository;
            _configuration = configuration;
        }




        [HttpPost("Register")]
        public ActionResult validate(Account account)
        {
            if (string.IsNullOrWhiteSpace(account.NIK) || string.IsNullOrWhiteSpace(account.Password))
            {
                return BadRequest("NIK dan Password tidak boleh kosong");
            }

            if (string.IsNullOrWhiteSpace(account.NIK))
            {
                return BadRequest("NIK tidak boleh kosong");
            }

            if (string.IsNullOrWhiteSpace(account.Password))
            {
                return BadRequest("Password tidak boleh kosong");
            }

            accountRepository.Insert(account);

            return Ok("Input data berhasil");
        }

        [HttpGet("Cek Akun Teregister")]
        public ActionResult Get()
        {
            var get = accountRepository.Get();
            if (get.Count() == 0 || get == null)
            {
                return StatusCode(204, new { status = HttpStatusCode.NoContent, message = "Data tidak dapat ditampilkan", Data = get });
            }
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Semua data akan tampil", Data = get });

        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var login = accountRepository.Login(loginVM);
            if (login == null || !BCrypt.Net.BCrypt.Verify(loginVM.Password, login.Password))
            {
                return StatusCode(400, new { status = HttpStatusCode.NoContent, message = "Email atau Password salah" });
            }
            var token = JsonWebToken(login.Employee);
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data tampil", Token = token, Data = login });
        }


        //public ActionResult Login(string email, string password)
        //{
        //    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        //    {
        //        return StatusCode(404, new { status = HttpStatusCode.NoContent, message = "Email atau Password tidak boleh kosong", Data = 0 });
        //    }
        //    var acc = accountRepository.Get().ToList();
        //    var get = acc.FirstOrDefault(e => e.Employee.Email == email);
        //    if(get == null || !BCrypt.Net.BCrypt.Verify(password, get.Password))
        //    {
        //        return StatusCode(400, new { status = HttpStatusCode.NoContent, message = "Email atau Password salah", Data = 0});
        //    }
        //    var token = CreateToken(get.Employee);
        //    return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data tampil", Data = token });
        //}



        private string JsonWebToken(Employee employee)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("NIK", employee.NIK),
                new Claim("FirstName", employee.FirstName),
                new Claim("Lastname", employee.LastName),
                new Claim("Email", employee.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //HmacSha256 merupakan header
                    
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: cred); //Verify Signature

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
