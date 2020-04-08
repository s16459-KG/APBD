using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        //Zadanie 4
        [HttpGet("{id}")]
        public IActionResult GetStudents(int id)
        {
            if (id == 1)
            {
                return Ok("Brawo");
            }
            if (id == 2)
            {
                return Ok("Git Malina");
            }
            return NotFound("Ni ma");
           
        }

        //Zadanie 5
        [HttpGet]
        public string GetStudents([FromQuery]string orderBy)
        {
            return $"Kowalski, Malewski, Andrzejewski sortowanie={orderBy}";
        }

        //Zadanie 6
        [HttpPost]
        public IActionResult CreateStudent([FromBody]Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        //Zadanie 7
        [HttpPut("{id}")]
        public IActionResult PutStudent(int id)
        {
    
            return Ok("Aktualizacja dokończona " + id );
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie ukonczone " + id);
        }

    }
}