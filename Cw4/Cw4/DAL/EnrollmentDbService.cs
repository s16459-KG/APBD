using Cw4.DTOs.Requests;
using Cw4.DTOs.Responses;
using Cw4.ModelsBaza;
using Cw4.Models;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cw4.DAL
{
    public class EnrollmentDbService : IEnrollmentDbService
    {

    private const string SqlConn = "Data Source=db-mssql;Initial Catalog=s16459;Integrated Security=True";

        public EnrollStudentResponse EnrollStudents(EnrollStudentRequest request)
        {
            //Mapowanie
            var student = new Student();
            student.IndexNumber = request.IndexNumber;
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.BirthDate = DateTime.Parse(request.BirthDate);

            //tworzymy odpowiedz dla klienta
            var enrolment = new EnrollStudentResponse();
            enrolment.Semester = 1;

            int lastEnrollmentId = 0;

            //Inicjacja Kontekstu Entity FrameWork
            var db = new s16459Context();

            //Sprawdzamy czy istnieją studia o podanej nazwie
            var wynik =  db.Studies.Where(e => e.Name == request.Studies);
            if (wynik == null)
            {
                return null;
            }
            
            //Przypisujemy id studiów
            var idStudies = wynik.ToList().Last().IdStudy;
            enrolment.studia = request.Studies;

            //Sprawdzamy czy istnieje już wpis dla danych studiów na dany semestr
            var wynik1 = db.Enrollment.Where(e => e.IdStudy == idStudies & e.Semester == 1);
            if (wynik1 == null) {
                //Sprawdzenie ostatniego numeru idEnrollment aby dodać z o 1 większym kolejny wpis
                var wynik2 = db.Enrollment.Select(e => e.IdEnrollment).Max();
                //Dodanie nowego Enrollmentu do bazy
                db.Enrollment.Add(new Enrollment(wynik2, 1, idStudies, DateTime.Now));
                student.IdEnrollment = lastEnrollmentId + 1;
            }
            else
            {
                lastEnrollmentId = (int)wynik1.Select(s => s.IdEnrollment).First();
                student.IdEnrollment = lastEnrollmentId;
            }

            enrolment.IdEnrollment = student.IdEnrollment;

            var wynik3 = db.Student.Where(s => s.IndexNumber == student.IndexNumber).Select(s => s.IndexNumber);

            if (wynik3.FirstOrDefault() == null)
            {
                db.Student.Add(student);
                db.SaveChanges();
            }
            else
            {
                return null;
            }
            return enrolment;
        }

        public Enrollment PromoteStudents(PromoteStudentsRequest request)
        {
            //Mapowanie
            var enrolment = new Enrollment();
            enrolment.Semester = request.Semester;

            //Inicjacja Kontekstu Entity FrameWork
            var db = new s16459Context();

            Console.WriteLine(1);

            var studiesParam = new SqlParameter("@Studies", request.Studies);
            var semesterParam = new SqlParameter("@Semester", enrolment.Semester);

            var returnValue = db.Database.ExecuteSqlCommand("PromoteStudents @Studies, @Semester", studiesParam, semesterParam);

            if (returnValue == -1)
            {
                return new EnrolmentError("Brak przedmiotu lub konkretnego semestru");
            }
            Console.WriteLine(2);

            var wynik = db.Studies.Where(s => s.Name == request.Studies)
                .Join(
                    db.Enrollment.Where(e => e.Semester == enrolment.Semester),
                    e => e.IdStudy,
                    study => study.IdStudy,
                    (e, study) => new
                    {
                        IdEnrollment = study.IdEnrollment,
                        IdStudy = e.IdStudy,
                        StartDate = study.StartDate,

                    }).ToList();

            if (wynik.FirstOrDefault() != null)
            {
                enrolment.IdEnrollment = wynik.FirstOrDefault().IdEnrollment;
                enrolment.StartDate = wynik.FirstOrDefault().StartDate;
                enrolment.IdStudy = wynik.FirstOrDefault().IdStudy;
            }
            db.SaveChanges();
            return enrolment;
        }


        public bool CheckIndex(string index)
        {
            using (var connection = new SqlConnection(SqlConn))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();

                command.CommandText = "Select IndexNumber from Student where IndexNumber = @index";
                command.Parameters.AddWithValue("index", index);
                var dr = command.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    return true;
                }
                dr.Close();
                return false;
            }
        }

        public static bool CheckIndexAndPassword(string index, string password)
        {
            using (var connection = new SqlConnection(SqlConn))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();

                command.CommandText = "Select IndexNumber from Student where IndexNumber = @index and password = @password";
                command.Parameters.AddWithValue("index", index);
                command.Parameters.AddWithValue("password", password);
                var dr = command.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    return true;
                }
                dr.Close();
                return false;
            }
        }

    }
}
