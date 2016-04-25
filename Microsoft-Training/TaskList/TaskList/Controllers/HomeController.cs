using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskList.Models;

namespace TaskList.Controllers
{
    public class HomeController : Controller
    {
        private TaskListDataContext db = new TaskListDataContext();

        // Display a list of tasks
        public ActionResult Index()
        {
            var tasks = from t in db.Tasks orderby t.EntryDate descending select t;
            return View(tasks.ToList());
        }

        //Display a form for creating a new task
        public ActionResult Create()
        {
            return View();
        }

        //Adding a new task to the database
        public ActionResult CreateNew(string task)
        {
            Task newTask = new Task();
            newTask.Task1 = task;
            newTask.IsCompleted = false;
            newTask.EntryDate = DateTime.Now;

            db.Tasks.InsertOnSubmit(newTask);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        //Mark a task as complete
        public ActionResult Complete(int Id)
        {
            var tasks = from t in db.Tasks where t.Id == Id select t;
            foreach(Task match in tasks)
            {
                match.IsCompleted = true;
            }

            db.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}