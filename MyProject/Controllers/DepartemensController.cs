using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Context;
//using MyProject.Migrations;
using MyProject.Models;
using MyProject.Repository;
using MyProject.Repository.Interface;
using System.Net;

namespace MyProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartemensController : ControllerBase
    {
        private readonly DepartemenRepository departemenRepository;

        public DepartemensController(DepartemenRepository departemenRepository)
        {
            this.departemenRepository = departemenRepository;
        }

        [HttpPost]
        public ActionResult Insert(Departemen departemen)
        {
            if (string.IsNullOrWhiteSpace(departemen.Nama_Departemen))
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Nama Departemen Tidak Boleh Kosong"});
            }
            var dep = departemenRepository.Insert(departemen);
            if (dep >= 1)
            {
                return StatusCode(201, new { status = HttpStatusCode.OK, message = "Data Berhasil disimpan"});
            }
            return StatusCode(401, new { status = HttpStatusCode.BadRequest, message = "Data Tidak Berhasil Diinput" });

        }

        [HttpGet]
        public ActionResult Get()
        {
            var get = departemenRepository.Get();
                if (get.Count() == 0 || get == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.NoContent, message = "Tidak ada data yang dapat ditampilkan", Data = get });   
            }
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data tampil", Data = get });
        }

        [HttpGet("{Id_Departemen}")]
        public ActionResult Get(int Id_Departemen)
        {
            var get = departemenRepository.Get(Id_Departemen);
            if (get == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Data Tidak Ditemukan" });
            }
             return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data tampil", Data = get });
        }

        [HttpDelete("{Id_Departemen}")]
        public ActionResult Delete(int Id_Departemen)
        {
            var nik = departemenRepository.Get(Id_Departemen);
            if (nik == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Id Departemen yang dimasukkan salah, data tidak dapat dihapus" });
            }
            departemenRepository.Delete(Id_Departemen);
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data sudah berhasil dihapus", Data = nik });
        }

        [HttpPut("Update")]
        public ActionResult Update(Departemen departemen)
        {
            if (departemenRepository.IsIdExists(departemen.Id_Departemen))
            {
                departemenRepository.Update(departemen);
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Sudah Terupdate", Data = departemen });
            }
            return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Id Departemen yang dimasukkan salah" });
        }

    }
}
