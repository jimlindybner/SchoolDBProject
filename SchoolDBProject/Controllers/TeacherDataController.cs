using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolDBProject.Models;
using MySql.Data.MySqlClient;

namespace SchoolDBProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        //The DatabaseContext class which allows us to access our MySQL database.
        private SchoolDBContext School = new SchoolDBContext();

        //This controller accesses teachers table in MySQL database
        /// <summary>
        /// Returns a list of teachers in the system
        /// </summary>
        /// 
        /// <param name="q">
        /// text to search against the teacher names
        /// </param>
        /// 
        /// <example>
        /// GET api/TeacherData/ListTeachers -> Alexander Bennett
        /// </example>
        /// 
        /// <returns>
        /// A list of teachers (first and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{q}")]
        public IEnumerable<Teacher> ListTeachers(string q)
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the wbserver and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL query
            string query = "SELECT * FROM teachers WHERE LOWER(teacherfname) LIKE @q OR LOWER(teacherlname) LIKE @q;";
            Command.CommandText = query;
            Command.Parameters.AddWithValue("@q", $"%{q}%");
            Command.Prepare();

            //Gather result set of query into variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop through each row in result set to add teacher info to list of Teachers
            while (ResultSet.Read())
            {
                //Access column info by DB column name as index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNum = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNum = EmployeeNum;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //Add teacher info to list
                Teachers.Add(NewTeacher);
            }

            //Close connection between wbserver and database
            Connection.Close();

            //return final list of teachers
            return Teachers;
        }

        //This controller looks for specific record using id input
        [HttpGet]
        [Route("api/TeacherData/ShowTeacher/{id}")]
        public Teacher ShowTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the wbserver and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL query
            Command.CommandText = $"SELECT * FROM teachers WHERE teacherid = {id};";

            //Gather result set of query into variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access column info by DB column name as index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNum = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNum = EmployeeNum;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }
            
            return NewTeacher;
        }

        /// <summary>
        /// This method doesn't return anything but sends query/command to database to delete a record using id input
        /// </summary>
        /// <param name="id"></param>
        /// <example>
        /// POST : api/TeacherData/DeleteTeacher/3 -> doesn't return anything, sends delete query to database
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/DeleteTeacher/{id}")]
        public void DeleteTeacher(int id)
        {
            //create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //open the connection between the webserver and the database
            Connection.Open();

            //establish a new command (query) for our database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL query
            Command.CommandText = "DELETE FROM teachers WHERE teacherid = @id";
            Command.Parameters.AddWithValue("@id", id);
            Command.Prepare();

            Command.ExecuteNonQuery();

            //close connection - nothing is returned as this is a void method - just execute deletion

            Connection.Close();
        }

        [HttpPost]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            //create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //open the connection between the webserver and the database
            Connection.Open();

            //establish a new command (query) for our database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL query
            Command.CommandText =
                "INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary)" + 
                "VALUES (@TeacherFname, @TeacherLname, @EmployeeNum, @HireDate, @Salary)";

            Command.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            Command.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            Command.Parameters.AddWithValue("@EmployeeNum", NewTeacher.EmployeeNum);
            Command.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            Command.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            Command.Prepare();
            Command.ExecuteNonQuery();

            //close connection - nothing is returned as this is a void method - just execute deletion
            Connection.Close();
        }
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            //create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //open the connection between the webserver and the database
            Connection.Open();

            //establish a new command (query) for our database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL query
            Command.CommandText =
                "Update teachers SET " +
                "teacherfname=@TeacherFname, " +
                "teacherlname=@TeacherLname, " +
                "employeenumber=@EmployeeNum, " +
                "hiredate=@HireDate, " +
                "salary=@Salary " +
                "WHERE teacherid=@TeacherId";
            Command.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            Command.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            Command.Parameters.AddWithValue("@EmployeeNum", TeacherInfo.EmployeeNum);
            Command.Parameters.AddWithValue("@HireDate", TeacherInfo.HireDate);
            Command.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            Command.Parameters.AddWithValue("@TeacherId", id);
            Command.Prepare();

            Command.ExecuteNonQuery();

            //close connection - nothing is returned as this is a void method - just execute deletion
            Connection.Close();
        }
    }
}
