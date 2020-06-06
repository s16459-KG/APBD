using Cwiczenia11.DAL;
using Cwiczenia11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cwiczenia11.Controllers
{

    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : ControllerBase
    {

        private readonly IDoctorDbService _doctorDbService;
        public IConfiguration Configuration { get; set; }

        public DoctorController(IDoctorDbService doctortDbService, IConfiguration configuration)
        {
            _doctorDbService = doctortDbService;
            Configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok(_doctorDbService.GetDoctors());
        }

        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            return Ok(_doctorDbService.AddDoctor(doctor));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            return Ok(_doctorDbService.DeleteDoctor(id));
        }

        [HttpPost("modify/{id}")]
        public IActionResult ModifyDoctor(Doctor doctor)
        {
            return Ok(_doctorDbService.ModifyDoctor(doctor));
        }

    }
}