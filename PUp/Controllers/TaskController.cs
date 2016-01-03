using PUp.Models.Entity;
using PUp.Models.Repository;
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

        ITaskRepository taskRepository;
        IProjectRepository projectRepository;

        /// <summary>
        /// @todo: Use dependency injection
        /// </summary>
        public TaskController()
        {
            taskRepository = new TaskRepository();
            projectRepository = new ProjectRepository();
            projectRepository.SetDbContext(taskRepository.GetDbContext());
        }

        // GET: Task
        public ActionResult Index(int id)
        {        
            ProjectEntity project = projectRepository.FindById(id);
            return View(project);
        }

        public ActionResult MarkDone(int id)
        {
            TaskEntity project = taskRepository.FindById(id);
            return View(project);
        }

        public ActionResult Add(int id)
        {
            ProjectEntity project = projectRepository.FindById(id);
            AddTaskViewModel addTaskVM = new AddTaskViewModel { Project = project, IdProject= project.Id };
            return View(addTaskVM);
        }

        [HttpPost]
        public ActionResult Add(AddTaskViewModel model)
        { 
            ProjectEntity project = projectRepository.FindById(model.IdProject);
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
            taskRepository.Add(task);
            project.Tasks.Add(task);  
            taskRepository.GetDbContext().SaveChanges();
            return RedirectToAction("Index", "Task", new {id=project.Id });
        }
    }
}