using Cw4.DAL;
using Cw4.DTOs.Requests;
using Cw4.ModelsBaza;
using Cw4.Pomocnicze;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cw4.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private static int licznik = 0;
        private readonly IStudentDbService _studentDbService;
        public IConfiguration Configuration { get; set; }

        public StudentsController(IStudentDbService studentDbService, IConfiguration configuration)
        {
            _studentDbService = studentDbService;
            Configuration = configuration;

        }

        [HttpGet("{id}")]
        public IActionResult GetEnrollment(string id)
        {
            return Ok(_studentDbService.GetEnrollment(id));
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            Console.WriteLine("Licznik = " + ++licznik);
            return Ok(_studentDbService.GetStudents());
        }

        [HttpPost("modify")]
        public IActionResult ModifyStudent(Student student)
        {
            return Ok(_studentDbService.ModifyStudent(student));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(String id)
        {
            return Ok(_studentDbService.DeleteStudent(id));
        }


        [HttpPost("refresh-token/{token}")]
        [AllowAnonymous]
        public IActionResult RefreshToken(string token)
        {
            Student student = StudentDbService.CheckRefToken(token);
            if (student == null) return BadRequest();
            return Login(new LoginRequest(student.IndexNumber, student.Password));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest request)
        {
            Student student = StudentDbService.GetStudent(request.Login);
            //Console.WriteLine(PasswordHelper.CreateSecretValue(student.password, student.salt));
            if(!PasswordHelper.Validate(request.Haslo, student.Salt, student.Password))
                return BadRequest("Błędne hasło");

            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier,student.IndexNumber),
                 new Claim(ClaimTypes.Name,student.FirstName),
                 new Claim(ClaimTypes.Surname,student.LastName),
                 new Claim(ClaimTypes.DateOfBirth,student.BirthDate.ToString()),
                 new Claim(ClaimTypes.Role, "employee")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: "Gakko",
                    audience: "Students",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: creeds
                );

            var refreshToken = Guid.NewGuid();
            DateTime dateTime = DateTime.Now;  
            StudentDbService.AddRefreshToken(student, refreshToken.ToString(), dateTime.AddDays(1));

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken
            });

        }
        

    }
}