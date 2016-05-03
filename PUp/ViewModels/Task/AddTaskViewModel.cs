using PUp.Helpers;
using PUp.Models;
using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUp.ViewModels.Task
{
    /// <summary>
    /// TODO: Add more custom validations 
    /// </summary>
    public class AddTaskViewModel : BaseModelView
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be between {1} and {2} caracters.", MinimumLength = 30)]
        public string Title { get; set; }

        [Required]
        [StringLength(100000, ErrorMessage = "The {0} must be between {1} and {2} caracters.", MinimumLength = 30)]
        public string Description { get; set; }

        public bool KeyFactor { get; set; }

        [Required]
        public int EstimatedTimeInMinutes { get; set; }


        public List<SimpleKeyValue<int, string>> EstimatedMinList = new List<SimpleKeyValue<int, string>>();

        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }

         
        public bool Urgent { get; set; }


       
        public bool Critical { get; set; }

         
        public List<SimpleKeyValue<string, string>> UsersList = new List<SimpleKeyValue<string, string>>();

        [Required]
        public string ExecutorId { set; get; }
        [Required]
        public int ProjectId { get; set; }         
        public GroundInterval AvailableDates { get; set; }

        public AddTaskViewModel()
        {
            //No parameterless constructor/...
        }
        public AddTaskViewModel(int projectId, List<UserEntity> users)
        {
            Id = 0;
            InitElements(projectId, users);
        }

        /// <summary>
        /// Useful on Edit!
        /// </summary>
        /// <param name="task"></param>
        /// <param name="users"></param>
        public AddTaskViewModel(TaskEntity task, List<UserEntity> users)
        {
            InitElements(task.Project.Id, users);
            InitByTask(task);
        }


        public void InitByTask(TaskEntity task)
        {
            Description = task.Description;
            EstimatedTimeInMinutes = task.EstimatedTimeInMinutes;
            ExecutorId = task.Executor.Id;
            Critical = task.Critical;
            KeyFactor = task.KeyFactor;
            StartAt = task.StartAt;
            EndAt = task.EndAt;
            Title = task.Title;
            Urgent = task.Urgent;
            Id = task.Id;
        }


        public void InitElements(int idProject, List<UserEntity> users)
        {
            ProjectId = idProject;
            KeyFactor = false;
            StartAt = DateTime.Now.AddHours(1);

           
            EstimatedMinList.Add(new SimpleKeyValue<int,string> { Key =60, Value= "1H" });
            EstimatedMinList.Add(new SimpleKeyValue<int,string> { Key =120, Value= "2H" });
            EstimatedMinList.Add(new SimpleKeyValue<int,string> { Key =180, Value= "3H" });
 
            users.ForEach(u => UsersList.Add(new SimpleKeyValue<string, string> {Key=u.Id, Value=u.Email }));
        }
    }


}