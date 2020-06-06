using Cw4.DTOs.Requests;
using Cw4.DTOs.Responses;
using Cw4.ModelsBaza;

public interface IEnrollmentDbService
    {
    public EnrollStudentResponse EnrollStudents(EnrollStudentRequest request);

    public Enrollment PromoteStudents(PromoteStudentsRequest request);

    public bool CheckIndex(string index);

    }