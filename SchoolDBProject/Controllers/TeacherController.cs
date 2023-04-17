using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolDBProject.Controllers;
using SchoolDBProject.Models;

namespace SchoolDBProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/List/{q}
        [HttpGet]
        [Route("Teacher/List/{q}")]
        public ActionResult List(string q)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(q);
            return View(Teachers);
        }

        //GET: Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.ShowTeacher(id);

            return View(NewTeacher);
        }

        //Not actually executing the DELETE query. Just sending user to a confirmation page.
        //GET: Teacher/DelConfirmation/{id}
        public ActionResult DelConfirmation(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.ShowTeacher(id);

            return View(NewTeacher);
        }

        //Actually executing the DELETE query should the user confirm.
        //POST: Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }

        //GET: Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //GET: Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNum, DateTime HireDate, decimal Salary)
        {
            //debug
            Debug.WriteLine("Input received from 'Add Teacher' form!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNum);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);

            //instantiate new teacher and assign values to object properties
            Teacher NewTeacher = new Teacher();
            
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNum = EmployeeNum;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            //pass new data to AddTeacher method in TeacherDataController
            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            //redirect to list
            return RedirectToAction("List");
        }

        //GET : Teacher/Update/{id}
        /// <summary>
        /// Routes to dynamically generated "Teacher Update" page.
        /// Gathers info from database.
        /// </summary>
        /// <param name="id">id of teacher</param>
        /// <returns>
        /// dynamic "Update Teacher" page which provides the current information of teacher
        /// and asks the user for new information as part of a form.
        /// </returns>
        [HttpGet]
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.ShowTeacher(id);

            return View(SelectedTeacher);
        }

        //POST : Teacher/Update/{id}
        /// <summary>
        /// Receives a POST request containting information about an existing teacher in the system, with new values.
        /// Convesy this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">id of teacher to update</param>
        /// <param name="TeacherFname">updated first name of teacher</param>
        /// <param name="TeacherLname">updated last name of teacher</param>
        /// <param name="EmployeeNum">updated bio of teacher</param>
        /// <param name="HireDate">updated email of teacher</param>
        /// <param name="Salary">updated salary of teacher</param>
        /// <returns>
        /// dynamic webpage which provides current info of teacher
        /// </returns>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNum, DateTime HireDate, decimal Salary)
        {
            //debug
            Debug.WriteLine("Input received from 'Add Teacher' form!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNum);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);

            //instantiate new teacher and assign values to object properties
            Teacher TeachrInfo = new Teacher();

            TeachrInfo.TeacherFname = TeacherFname;
            TeachrInfo.TeacherLname = TeacherLname;
            TeachrInfo.EmployeeNum = EmployeeNum;
            TeachrInfo.HireDate = HireDate;
            TeachrInfo.Salary = Salary;

            //pass new data to AddTeacher method in TeacherDataController
            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeachrInfo);

            //redirect to show/id
            return RedirectToAction("Show/" + id);
        }
    }
}