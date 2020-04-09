using Cw4.Models;
using System.Collections.Generic;

namespace Cw4.DAL
{
    public interface IStudentDbService
    {
        IEnumerable<Student> GetStudents();

        int GetEnrollment(string id);
    }
}
