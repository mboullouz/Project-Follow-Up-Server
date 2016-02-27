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

        public int EstimatedTimeInMinutes { get; set; }
        public SelectList EstimatedMinList { get; set; }

        public DateTime? StartAt { get; set; }

        public SelectList UrgentList { get; set; }
        public bool Urgent { get; set; }


        public SelectList ImportantList { get; set; }
        public bool Important { get; set; }

        public List<UserEntity> Users = new List<UserEntity>();
        public SelectList UsersList { get; set; }

        //Data
        public string ExecutorId { set; get; }
        public int IdProject { get; set; }
        public ProjectEntity Project { get; set; }
        public GroundInterval AvelaibleDates { get; set; }

        public AddTaskViewModel()
        {
            //No parameterless constructor/...
        }
        public AddTaskViewModel(int idProject, List<UserEntity> users)
        {
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
            IdProject = idProject;
            Users = users;

            KeyFactor = false;
            StartAt = DateTime.Now.AddHours(1);

            EstimatedMinList = new SelectList(
               new List<SelectListItem>
               {
                    new SelectListItem { Selected = true, Text  = "1H" ,   Value = "60"},
                    new SelectListItem { Selected = false, Text = "2H",   Value = "120"},
                    new SelectListItem { Selected = false, Text = "3H",   Value = "180"},
                    new SelectListItem { Selected = false, Text = "4H",   Value = "240"},
               }, "Value", "Text", 1);

            UrgentList = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Selected = true,  Text = "Yes",   Value = "true"},
                    new SelectListItem { Selected = false, Text = "No" ,   Value = "false"},
                }, "Value", "Text", 1);

            ImportantList = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Selected = true,  Text = "Yes", Value = "true"},
                    new SelectListItem { Selected = false, Text = "No" ,   Value = "false"},
                }, "Value", "Text", 1);

            var items = new List<SelectListItem>();
            Users.ForEach(u => items.Add(new SelectListItem { Selected = true, Text = u.Name, Value = u.Id }));

            UsersList = new SelectList(items, "Value", "Text", 1);
        }
    }
}