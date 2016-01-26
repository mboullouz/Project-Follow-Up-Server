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
        public Nullable<int> Priority { get; set; }

        public SelectList PriorityList { get; set; }

        [Required]
        [Display(Name = "Task done ?")]
        public bool Done { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime EditAt { get; set; }
        public Nullable<DateTime> FinishAt { get; set; }
        public int IdProject { get; set; } 
        public ProjectEntity Project { get; set; }
        public bool keyFactor { get; set; }
        public int estimatedTimeInMinutes { get; set; }

        public SelectList EstimatedMinList { get; set; } 

        public AddTaskViewModel()
        {
            Done = false;
            keyFactor = false;
            PriorityList = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Selected = true,  Text = "Normal", Value = "1"},
                    new SelectListItem { Selected = false, Text = "Low" ,   Value = "0"},
                    new SelectListItem { Selected = false, Text = "High",   Value = "2"},
                }, "Value", "Text", 1);
            EstimatedMinList = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Selected = false,  Text ="30 min", Value = "60"},
                    new SelectListItem { Selected = true, Text  = "1H" ,   Value = "60"},
                    new SelectListItem { Selected = false, Text = "2H",   Value = "120"},
                    new SelectListItem { Selected = false, Text = "3H",   Value = "180"},
                    new SelectListItem { Selected = false, Text = "4H",   Value = "240"},
                    new SelectListItem { Selected = false, Text = "1 day",   Value = "420"},
                    new SelectListItem { Selected = false, Text = "2 days",   Value = "840"},
                }, "Value", "Text", 1);
            CreateAt = DateTime.Now;
            EditAt = DateTime.Now;
        }
    }
}