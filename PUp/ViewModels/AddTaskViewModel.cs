using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PUp.ViewModels
{
    public class AddTaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> Priority { get; set; }

        [Required]
        [Display(Name = "Task done ?")]
        public bool Done { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime EditAt { get; set; }
        public Nullable<DateTime> FinishAt { get; set; }
        public int IdProject { get; set; } 
        public ProjectEntity Project { get; set; }

        public AddTaskViewModel()
        {
            Done = false;
            CreateAt = DateTime.Now;
            EditAt = DateTime.Now;
        }
    }
}