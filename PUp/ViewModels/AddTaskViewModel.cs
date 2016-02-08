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
        public string Title { get; set; }
        public string Description { get; set; }
        

        public SelectList PriorityList { get; set; }

        [Required]
        [Display(Name = "Task done ?")]
        public bool Done { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime EditAt { get; set; }
        public Nullable<DateTime> FinishAt { get; set; }
        public int IdProject { get; set; } 
        public ProjectEntity Project { get; set; }
        public bool KeyFactor { get; set; }
        public int EstimatedTimeInMinutes { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndAt { get; set; }
       


        public SelectList UrgentList { get; set; }
        public bool Urgent { get; set; }


        public SelectList ImportantList { get; set; }
        public bool Important { get; set; }

        public List<UserEntity> Users = new List<UserEntity>();
        public SelectList UsersList { get; set; }
        
        public string ExecutorId { set; get; }
        public AddTaskViewModel()
        {
            //No parameterless constructor/...
        }
        public AddTaskViewModel(int idProject, List<UserEntity>users)
        {
            IdProject = idProject;
            Users = users;
            Done = false;
            KeyFactor = false;
            StartAt = DateTime.Now.AddHours(1);
            EndAt = DateTime.Now.AddHours(3);
    

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
            Users.ForEach(u => items.Add(new SelectListItem { Selected = true, Text = u.Email, Value = u.Id }));

            UsersList = new SelectList(items, "Value", "Text", 1);

            CreateAt = DateTime.Now;
            EditAt = DateTime.Now;
        }
    }
}