using PUp.Models.Entity;
using PUp.Models.Facade;
using PUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUp.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index(int id)
        {
            ProjectFacade pf = new ProjectFacade();
            ProjectEntity project = pf.FindById(id);

            return View(project);
        }
        public ActionResult Add(int id)
        {
            ProjectFacade pf = new ProjectFacade();
            ProjectEntity project = pf.FindById(id);
            AddTaskViewModel addTaskVM = new AddTaskViewModel { Project = project, IdProject= project.Id };
            return View(addTaskVM);
        }

        [HttpPost]
        public ActionResult Add(AddTaskViewModel model)
        {

            ProjectFacade pf = new ProjectFacade();
            TaskFacade tf = new TaskFacade();
            tf.SetDbContext(pf.GetDbContext());
            ProjectEntity project = pf.FindById(model.IdProject);

            if (!ModelState.IsValid )
            {
                model.Project = project;
                return View(model);
            }
            
            TaskEntity task = new TaskEntity
            {
                Title = model.Title,
                Description = model.Description,
                Priority = model.Priority,
                Done = model.Done,
                Project = project,
                CreateAt =  DateTime.Now,
                EditAt = DateTime.Now,
                EditionNumber= 1,
            };
            tf.Add(task);
            project.Tasks.Add(task);  
            tf.GetDbContext().SaveChanges();


            return RedirectToAction("Index", "Task", new {id=project.Id });
        }
    }
}