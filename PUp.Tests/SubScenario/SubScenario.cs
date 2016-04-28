using PUp.Models;
using PUp.Models.Entity;
using PUp.ViewModels.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUp.Tests.SubScenario
{
    public class SubScenarioBuilder
    {
        private  RepositoryManager rep;
        
        public SubScenarioBuilder(RepositoryManager rep)
        {
            this.rep = rep;
        }

        public   ProjectEntity PrepareInactiveProject(int projectId)
        {
            var project = rep.ProjectRepository.FindById(projectId);
            project.StartAt = DateTime.Now.AddHours(-100);
            project.EndAt = DateTime.Now.AddHours(-50);
            rep.DbContext.SaveChanges();
            return project;
        }

        public   ProjectEntity PrepareActiveProject(int projectId)
        {
            var project = rep.ProjectRepository.FindById(projectId);
            project.StartAt = DateTime.Now.AddHours(-10);
            project.EndAt = DateTime.Now.AddHours(50);
            rep.DbContext.SaveChanges();
            return project;
        }

        public TaskEntity UnpostponedAndRunningTask(int taskId)
        {
            var taskEntity = rep.TaskRepository.FindById(1);
            taskEntity.StartAt = DateTime.Now;
            taskEntity.EndAt = DateTime.Now.AddHours(2);
            taskEntity.Postponed = false;
            rep.DbContext.SaveChanges();
            return taskEntity;
        }

        
    }
}
