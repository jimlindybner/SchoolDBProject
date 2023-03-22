using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDBProject.Models
{
    public class Teacher
    {
        //following fields define a teacher
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public string EmployeeNum;
        public DateTime HireDate;
        public decimal Salary;
    }
}