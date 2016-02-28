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
            modelStateWrapper.Flash("Welcome in tasks section");
            return tVM;
        }
    }
}