using System;
using System.Collections.Generic;

namespace Cw4.ModelsBaza
{
    public partial class Enrollment
    {
        public Enrollment()
        {
            Student = new HashSet<Student>();
        }

        public Enrollment(int IdEnrollment, int Semester, int IdStudy, DateTime StartDate)
        {
            Student = new HashSet<Student>();
            this.IdEnrollment = IdEnrollment;
            this.Semester = Semester;
            this.IdStudy = IdStudy;
            this.StartDate = StartDate;
        }

        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public int IdStudy { get; set; }
        public DateTime StartDate { get; set; }

        public virtual Studies IdStudyNavigation { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
