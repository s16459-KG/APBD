using Cw4.DTOs.Requests;
using Cw4.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cw4.DAL
{
    public class StudentDbService : IStudentDbService
    {
        private static string SqlConn = "Data Source=db-mssql;Initial Catalog=s16459;Integrated Security=True";

        public int GetEnrollment(string id)
        {
            int output;
            using (var client = new SqlConnection(SqlConn))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = client;

                    //Sql injection np. komendą localhost:port/api/students/1 Drop table Student
                    command.CommandText = "Select IdEnrollment from Student Where IndexNumber = @id";
                    command.Parameters.AddWithValue("id", id);
                    client.Open();
                    var dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        return output = int.Parse(dr["IdEnrollment"].ToString());
                    }
                }
            }
            return -1;
        }

        public IEnumerable<Student> GetStudents()
        {
            var output = new List<Student>();
            using (var client = new SqlConnection(SqlConn))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = client;
                    command.CommandText = "Select * from Student";

                    client.Open();
                    var dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        output.Add(new Student
                        {
                            IndexNumber = dr["IndexNumber"].ToString(),
                            FirstName = dr["FirstName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            BirthDate = DateTime.Parse(dr["BirthDate"].ToString()),
                            IdEnrollment = int.Parse(dr["IdEnrollment"].ToString())
                        });
                    }
                }
            }
            return output;
        }

        public static Student GetStudent(string login, string haslo)
        {
            using (var client = new SqlConnection(SqlConn))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = client;
                    command.CommandText = "Select * from Student where indexnumber = @index and password = @password";
                    command.Parameters.AddWithValue("index", login);
                    command.Parameters.AddWithValue("password", haslo);

                    client.Open();
                    var dr = command.ExecuteReader();

                    if (!dr.Read()) return null;

                    return new Student
                    {
                        IndexNumber = dr["IndexNumber"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        BirthDate = DateTime.Parse(dr["BirthDate"].ToString()),
                        IdEnrollment = int.Parse(dr["IdEnrollment"].ToString())
                    };
                }
            }
        }

    }
}
