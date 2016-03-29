using PUp.Models;
using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUp.ViewModels
{
    public class AddTaskViewModel
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

        [Required]
        public IDictionary<int,string> EstimatedMinList { get; set; }

        public DateTime? StartAt { get; set; }

        public IDictionary<int, string> UrgentList { get; set; }
        public bool Urgent { get; set; }


        public IDictionary<int, string> ImportantList { get; set; }
        public bool Important { get; set; }

        public List<UserEntity> Users = new List<UserEntity>();
        public IDictionary<string, string> UsersList = new Dictionary<string, string>();

        //Data
        public string ExecutorId { set; get; }
        public int ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
        public GroundInterval AvailableDates { get; set; }

        public AddTaskViewModel()
        {
            //No parameterless constructor/...
        }
        public AddTaskViewModel(int idProject, List<UserEntity> users)
        {
            Id = 0;
            InitElements(idProject, users);
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
            Important = task.Critical;
            KeyFactor = task.KeyFactor;
            StartAt = task.StartAt;
            Title = task.Title;
            Urgent = task.Urgent;
            Id = task.Id;
        }


        public void InitElements(int idProject, List<UserEntity> users)
        {
            ProjectId = idProject;
            Users = users;

            KeyFactor = false;
            StartAt = DateTime.Now.AddHours(1);

            EstimatedMinList = new Dictionary<int, string>();
            EstimatedMinList[60] = "1H";
            EstimatedMinList[120] = "2H";
            EstimatedMinList[180] = "3H";



            UrgentList = new Dictionary<int, string>();
            UrgentList[0] = "false";
            UrgentList[1] = "true";

            ImportantList = new Dictionary<int, string>();
            ImportantList[0] = "false";
            ImportantList[1] = "true";
 

          
            Users.ForEach(u => UsersList[u.Id]=u.Email);

            
        }
    }
}