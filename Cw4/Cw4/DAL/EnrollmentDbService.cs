using Cw4.DTOs.Requests;
using Cw4.DTOs.Responses;
using Cw4.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Cw4.DAL
{
    public class EnrollmentDbService : IEnrollmentDbService
    {

    private const string SqlConn = "Data Source=db-mssql;Initial Catalog=s16459;Integrated Security=True";

        public EnrollStudentResponse EnrollStudents(EnrollStudentRequest request)
        {
            //Mapowanieroll
            var student = new Student();
            student.IndexNumber = request.IndexNumber;
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.BirthDate = DateTime.Parse(request.BirthDate);

            var enrolment = new EnrollStudentResponse();
            enrolment.Semester = 1;

            int idStudies = 0;
            int lastEnrollmentId = 0;

            using (var connection = new SqlConnection(SqlConn))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                var transaction = connection.BeginTransaction();

                //Sprawdzamy czy istnieją studia o podanej nazwie
                command.CommandText = "Select IdStudy from Studies where name = @studyName";
                command.Parameters.AddWithValue("studyName", request.Studies);
                command.Transaction = transaction;
                var dr = command.ExecuteReader();
                if (!dr.Read())
                {
                    dr.Close();
                    transaction.Rollback();
                    return null;
                }
                idStudies = (int)dr["IdStudy"];
                dr.Close();
                enrolment.studia = request.Studies;

                //Sprawdzamy czy istnieje już wpis dla danych studiów na dany semestr
                command.CommandText = "Select top 1 IdEnrollment from Enrollment where IdStudy = @idStudy " +
                    "and semester = 1 order by StartDate desc";
                command.Parameters.AddWithValue("idStudy", idStudies);
                dr = command.ExecuteReader();
                if (!dr.Read())
                {
                    //Sprawdzenie ostatniego numeru idEnrollment aby dodać z o 1 większym kolejny wpis
                    dr.Close();
                    command.CommandText = "Select max(IdEnrollment) as IdEnrollment from Enrollment";
                    dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        lastEnrollmentId = (int)dr["IdEnrollment"];
                    }
                    dr.Close();

                    //Dodanie nowego enrollmentu
                    command.CommandText = "Insert into Enrollment values(@idEnrollment, 1, @idStudies ,@actualDate )";
                    command.Parameters.AddWithValue("idEnrollment", lastEnrollmentId + 1);
                    command.Parameters.AddWithValue("idStudies", idStudies);
                    command.Parameters.AddWithValue("actualDate", DateTime.Now);
                    int dodaneWiersze = command.ExecuteNonQuery();
                    student.IdEnrollment = lastEnrollmentId + 1;
                }
                else
                {
                    lastEnrollmentId = (int)dr["IdEnrollment"];
                    student.IdEnrollment = lastEnrollmentId;
                    dr.Close();
                }

                enrolment.IdEnrollment = student.IdEnrollment;

                //Sprawdzamy czy indeks studenta jest unikalny
                command.CommandText = "Select IndexNumber from Student where IndexNumber = @iNumber";
                command.Parameters.AddWithValue("iNumber", student.IndexNumber);
                dr = command.ExecuteReader();
                if (!dr.Read())
                {
                    dr.Close();

                    //Jeżeli indeks jest unikalny to dodajemy studenta do bazy
                    command.CommandText = "Insert into Student (IndexNumber, firstname, lastname, birthdate, IdEnrollment ) " +
                        "values (@indexNumber, @firstName, @lastName, @birthDate, @idEnroll)";
                    command.Parameters.AddWithValue("indexNumber", student.IndexNumber);
                    command.Parameters.AddWithValue("firstName", student.FirstName);
                    command.Parameters.AddWithValue("lastName", student.LastName);
                    command.Parameters.AddWithValue("birthDate", student.BirthDate.ToShortDateString());
                    command.Parameters.AddWithValue("idEnroll", student.IdEnrollment);
                    int dodaneWiersze = command.ExecuteNonQuery();
                }
                else
                {
                    dr.Close();
                    transaction.Rollback();
                    return null;
                }

                if (!dr.IsClosed)
                {
                    dr.Close();
                }
                transaction.Commit();
                connection.Close();
            }
            return enrolment;
        }

        public Enrolment PromoteStudents(PromoteStudentsRequest request)
        {
            //Mapowanie
            var enrolment = new Enrolment();
            enrolment.Semester = request.Semester;

            using (var connection = new SqlConnection(SqlConn))
            using (var command = new SqlCommand("PromoteStudents", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Studies", request.Studies));
                command.Parameters.Add(new SqlParameter("@Semester", enrolment.Semester));
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                var returnValue = command.ExecuteNonQuery();
                if (returnValue == -1)
                {
                    transaction.Rollback();
                    return new EnrolmentError("Brak przedmiotu lub konkretnego semestru");
                }

                command.CommandType = CommandType.Text;
                command.CommandText = "Select IdEnrollment, e.IdStudy, StartDate " +
                                      " from Enrollment as e left join Studies as s on e.IdStudy = s.IdStudy " +
                                      " where Semester = @Semester2 + 1 and Name = @Studies2";
                command.Parameters.Add(new SqlParameter("@Studies2", request.Studies));
                command.Parameters.Add(new SqlParameter("@Semester2", enrolment.Semester));
                command.Transaction = transaction;
                var dr = command.ExecuteReader();

                if (dr.Read())
                {
                    enrolment.IdEnrollment = (int)dr["IdEnrollment"];
                    enrolment.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                    enrolment.IdStudy = (int)dr["IdStudy"];
                    dr.Close();
                }
                else
                {
                    transaction.Rollback();
                }
                transaction.Commit();
            }
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
