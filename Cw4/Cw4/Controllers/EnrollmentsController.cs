using Cw4.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cw4.Controllers
{
    [ApiController]
    //[Authorize(Roles = "employee")]
    public class EnrollmentsController : Controller
    {
        
        private IEnrollmentDbService _service;

        public EnrollmentsController(IEnrollmentDbService service)
        {
            _service = service;
        }

        [Route("api/enrollments")]
        [HttpPost]
        public IActionResult EnrollStudents(EnrollStudentRequest request)
        {
            if (_service.EnrollStudents(request) == null)
            {
                return BadRequest();
            }
            else
            {
                return new ObjectResult(request) { StatusCode = 201 };
            }
        }

        [Route("api/enrollments/promotions")]
        [HttpPost]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            if (_service.PromoteStudents(request) == null)
            {
                return BadRequest("Brak studiów w bazie");
            }
            else
            {
                return Ok(_service.PromoteStudents(request));
            }
        }
    }
}