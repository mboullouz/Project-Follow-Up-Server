using PUp.Models.Entity;
using PUp.ViewModels;
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

        public TaskViewModel GetTaskViewModelByProject(int id)
        {
            ProjectEntity project = repo.ProjectRepository.FindById(id);
            TaskViewModel tasksViewModel = new TaskViewModel(project);
            tasksViewModel.ActiveTasks = repo.DbContext.TaskSet.Include("Executor").Where(t => t.Deleted == false && t.Project.Id == id).ToList();
            tasksViewModel.DeletedTasks = repo.DbContext.TaskSet.Include("Executor").Where(t => t.Deleted == true && t.Project.Id == id).ToList();
            if (!repo.ProjectRepository.IsActive(project))
            {
                modelStateWrapper.Flash("You are browsering a project that is no more active!");
            }
            return tasksViewModel;
        }
        public TaskEntity MarkDone(int id)
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

        public void SetDateForTask(int id)
        {
            var task = repo.TaskRepository.FindById(id);
            if (!repo.ProjectRepository.IsActive(task.Project.Id))
            {
                modelStateWrapper.Flash("The project is no more active!", FlashLevel.Warning);
                return;
            }
            var intervalManager = repo.TaskRepository.AvelaibleHoursForUserAndDate(currentUser, DateTime.Parse("00:00"));
            var level= FlashLevel.Warning;
            string message = "The task can't fit in the remaining time";
            foreach (var vK in intervalManager.Interval.ToList())// To list is needed because the interval is modified during iteration!
            {
                string dateStartStr = vK.Key + ":00"; //Checks start from this str date till the end
                var dateStartForTest = DateTime.Parse(dateStartStr);
                if (!vK.Value && intervalManager.CheckForDateAndDuration(dateStartForTest, task.EstimatedTimeInMinutes / 60))
                {
                    task.StartAt = dateStartForTest;
                    repo.DbContext.SaveChanges();
                    level = FlashLevel.Success;
                    message = "Task added to the current day pile, Good luck!";
                    break;//no nead for more checks
                }
            }      
           modelStateWrapper.Flash(message, level);    
        }

        public AddTaskViewModel GetAddTaskViewModelByProject(int id) 
        {
            ProjectEntity project = repo.ProjectRepository.FindById(id);
            AddTaskViewModel addTaskVM = new AddTaskViewModel(project.Id, repo.UserRepository.GetAll());
            addTaskVM.Project = project;
            var currentHour = DateTime.Now.Hour + 1;
            addTaskVM.AvelaibleDates = repo.TaskRepository.AvelaibleHoursForUserAndDate(currentUser, DateTime.Now);
            return addTaskVM;
        }
    }
}