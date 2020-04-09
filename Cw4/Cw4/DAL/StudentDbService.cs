using Cw4.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cw4.DAL
{
    public class StudentDbService : IStudentDbService
    {
        private string SqlConn = "Data Source=db-mssql;Initial Catalog=s16459;Integrated Security=True";

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
                            IndexNumber = int.Parse(dr["IndexNumber"].ToString()),
                            FirstName = dr["FirstName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            BirthDate = dr["BirthDate"].ToString(),
                            IdEnrollment = int.Parse(dr["IdEnrollment"].ToString())
                        });
                    }
                }
            }
            return output;
        }
    }
}
