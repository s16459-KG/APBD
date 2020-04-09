using Cw4.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Cw4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentDbService _studentDbService;

        public StudentsController(IStudentDbService studentDbService)
        {
            _studentDbService = studentDbService;
        }

        [HttpGet("{id}")]
        public IActionResult GetEnrollment(string id)
        {
            return Ok(_studentDbService.GetEnrollment(id));
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_studentDbService.GetStudents());
        }

    }
}