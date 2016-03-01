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
        public TaskService(ModelStateWrapper modelStateWrapper) : base(modelStateWrapper)
        { }

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
            var level = FlashLevel.Warning;
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

        //TODO refactore AddTaskViewModel or create a special one for Edit with a base class!
        //Then Or merge the two actions 
        public AddTaskViewModel GetAddTaskViewModelByProject(int id)
        {
            ProjectEntity project = repo.ProjectRepository.FindById(id);
            if (!repo.ProjectRepository.IsActive(project.Id))
            {
                modelStateWrapper.Flash("This project is no more active, modifications won't be saved", FlashLevel.Warning);
            }
            AddTaskViewModel addTaskVM = new AddTaskViewModel(project.Id, repo.UserRepository.GetAll());
            addTaskVM.Project = project;
            addTaskVM.AvelaibleDates = repo.TaskRepository.AvelaibleHoursForUserAndDate(currentUser, DateTime.Now);
            return addTaskVM;
        }

        public AddTaskViewModel GetAddTaskViewModelByTask(int id)
        {
            TaskEntity task = repo.TaskRepository.FindById(id);
            ProjectEntity project = repo.ProjectRepository.FindById(task.Project.Id);
            if (!repo.ProjectRepository.IsActive(project.Id))
            {
                modelStateWrapper.Flash("This project is no more active, modifications won't be saved", FlashLevel.Warning);
            }
            AddTaskViewModel addTaskVM = new AddTaskViewModel(task, repo.UserRepository.GetAll());
            addTaskVM.AvelaibleDates = repo.TaskRepository.AvelaibleHoursForUserAndDate(currentUser, DateTime.Parse("00:01"));
            addTaskVM.Project = project;
            return addTaskVM;
        }

        public TaskEntity MarkUndone(int id)
        {

            var t = repo.TaskRepository.FindById(id);

            if (repo.ProjectRepository.IsActive(t.Project))
            {
                repo.TaskRepository.MarkUndone(t);
                modelStateWrapper.Flash("the task: " + t.Title + " is now marked undone!", FlashLevel.Info);
            }
            else
            {
                modelStateWrapper.Flash("The project is no more active! task state can't be modified", FlashLevel.Warning);
            }
            return t;
        }

        public TaskEntity Delete(int id)
        {
            var t = repo.TaskRepository.FindById(id);
            if (repo.ProjectRepository.IsActive(t.Project))
            {
                repo.TaskRepository.MarkDeleted(t);
                modelStateWrapper.Flash("Task: " + t.Title + " is marked  deleted! ", FlashLevel.Info);
            }
            else
            {
                modelStateWrapper.Flash("The project is no more active! task state can't be modified", FlashLevel.Warning);
            }

            return t;
        }

        public bool Add(AddTaskViewModel model)
        {
            ProjectEntity project = repo.ProjectRepository.FindById(model.IdProject);
            if (!modelStateWrapper.IsValid && !repo.ProjectRepository.IsActive(project))
            {
                modelStateWrapper.Flash("Can't save the task, The form is not valid Or you are trying to edit an inactive project", FlashLevel.Warning);
                return  false ;
            }
            var selectedUser = repo.UserRepository.FindById(model.ExecutorId);
            TaskEntity task = GetInitializedTaskFromModel(model);
            repo.TaskRepository.Add(task);
            project.Tasks.Add(task);
            project.Contributors.Add(selectedUser);
            project.Contributors.Add(currentUser);
            selectedUser.Tasks.Add(task);

            repo.NotificationRepository.GenerateFor(task, new HashSet<UserEntity> { currentUser, task.Executor });
            return true;
        }

        public TaskEntity GetInitializedTaskFromModel(AddTaskViewModel model)
        {  
            var selectedUser = repo.UserRepository.FindById(model.ExecutorId);
            TaskEntity task = new TaskEntity
            {
                Title = model.Title, 
                Description = model.Description,
                Done = false,
                Project = repo.ProjectRepository.FindById(model.IdProject),
                AddAt = DateTime.Now,
                EditAt = DateTime.Now,
                EstimatedTimeInMinutes = model.EstimatedTimeInMinutes,
                StartAt = model.StartAt,
                KeyFactor = model.KeyFactor,
                Deleted = false,
                Critical = model.Important,
                Urgent = model.Urgent,
                Executor = selectedUser
            };

            return task;
        }

    }
}