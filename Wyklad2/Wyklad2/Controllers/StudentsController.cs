using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wyklad2.Models;

namespace Wyklad2.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult GetStudents(Student st)
        {
            //var st = new Student();
            //st.FirstName = "Janko";
            //st.LastName = "z Bogdańca";

            return Ok(st);
        }
    }
}