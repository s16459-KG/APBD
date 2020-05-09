using System;
using System.ComponentModel.DataAnnotations;

namespace Cw4.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        [Required]
        [RegularExpression("^s[0-9]+$")]
        [MaxLength(100)]
        public String IndexNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public String BirthDate { get; set; }

        [Required]
        public String Studies { get; set; }
    }
}
