using PUp.Models.Entity;
using PUp.ViewModels.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Services
{
    public class TaskService : BaseService
    {
        public TaskService(ModelStateWrapper modelStateWrapper):base(modelStateWrapper)
        {}

        public TaskViewModel GetByProjectId(int id)
        {
            
            ProjectEntity project = repo.ProjectRepository.FindById(id);
            TaskViewModel tVM = new TaskViewModel(project);

            tVM.ActiveTasks = repo.DbContext.TaskSet.Include("Executor").Where(t => t.Deleted == false && t.Project.Id == id).ToList();
            tVM.DeletedTasks = repo.DbContext.TaskSet.Include("Executor").Where(t => t.Deleted == true && t.Project.Id == id).ToList();
            if (!repo.ProjectRepository.IsActive(project))
            {
                modelStateWrapper.Flash("You are browsering a project that is no more active!");
            }
             
            return tVM;
        }
        public TaskEntity MarkDoneById(int id)
        {
            var task = repo.TaskRepository.FindById(id);
            if (repo.ProjectRepository.IsActive(task.Project.Id))
            {
                repo.TaskRepository.MarkDone(task.Id);
            }
            else
            {
                modelStateWrapper.Flash("The project is no more active!", FlashLevel.Warning);
            }
            return task;
        }
    }
}