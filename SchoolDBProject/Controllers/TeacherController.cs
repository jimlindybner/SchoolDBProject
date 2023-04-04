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

            //instantiate new author and assign values to object properties
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
    }
}