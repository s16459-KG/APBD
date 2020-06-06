using Cw4.ModelsBaza;
using System.Collections.Generic;

namespace Cw4.DAL
{
    public interface IStudentDbService
    {
        IEnumerable<Student> GetStudents();

        int GetEnrollment(string id);
        Student ModifyStudent(Student student);
        string DeleteStudent(string id);
    }
}
