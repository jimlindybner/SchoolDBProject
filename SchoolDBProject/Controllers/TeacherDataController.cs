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
        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]
        public IEnumerable<Teacher> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the wbserver and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand Command = Connection.CreateCommand();

            //SQL query
            Command.CommandText = $"SELECT * FROM teachers;";

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
                NewTeacher.TeacherId= TeacherId;
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
    }
}
