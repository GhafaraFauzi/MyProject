using Microsoft.AspNetCore.Mvc;
using MyProject.Context;
using MyProject.Models;
using MyProject.Repository;
using MyProject.Repository.Interface;
using MyProject.ViewModels;
using System.Net;
using BCrypt.Net;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeesController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
            
        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var get = employeeRepository.Register(registerVM);
            if (get >= 1)
            {
                return StatusCode(201, new { status = HttpStatusCode.OK, message = "Data Berhasil disimpan", Data = get });
            }
            return StatusCode(400, new { status = HttpStatusCode.NoContent, message = "Data tidak berhasil disimpan", Data = get });
        }

        [HttpGet]
        public ActionResult Get()
        {
            var get = employeeRepository.Get();
            if (get == null)
            {
                return NotFound("Tidak ada data yang dapat ditampilkan");
            }
            return Ok(get);
        }

        [HttpGet("NIK")]
        public ActionResult Get(string NIK)
        {
            var get = employeeRepository.Get(NIK);
            if (get == null)
            {
                return NotFound("Data tidak ditemukan");
            }
            return Ok(get);
        }

        [HttpDelete]
        public ActionResult Delete(string NIK)
        {
            var nik = employeeRepository.Get(NIK);
            if (nik == null)
            {
                return NotFound("NIK yang dimasukkan salah, data tidak dapat dihapus");
            }
            employeeRepository.Delete(NIK);
            return Ok("Data sudah berhasil dihapus");
        }

        [HttpPut("Update")]
        public ActionResult Update(Employee employee)
        {
            if (employeeRepository.IsIdExists(employee.NIK))
            {
                employeeRepository.Update(employee);
                return Ok("Data Sudah Terupdate");
            }
            return NotFound("NIK yang dimasukkan salah");
        }

        [HttpGet("Gabungan")]

        public ActionResult Departemen()
        {
            var employee = employeeRepository.Departemen();
            return Ok(employee);
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            //return Ok("Test CORS berhasil");

            var response = new{message="Test CORS berhasil"};
            return Ok(response);
        }
    }
}