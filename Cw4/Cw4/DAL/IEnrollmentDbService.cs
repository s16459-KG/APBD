using Cw4.DTOs.Requests;
using Cw4.DTOs.Responses;
using Cw4.Models;

public interface IEnrollmentDbService
    {
    public EnrollStudentResponse EnrollStudents(EnrollStudentRequest request);

    public Enrolment PromoteStudents(PromoteStudentsRequest request);

    }