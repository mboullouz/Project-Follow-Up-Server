using PUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUp.ViewModels
{
    public class AddIssueViewModel
    {
        public int IdProject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public SelectList StatusList { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateAt { get; set; }
        public string RelatedArea { get; set; }

        public AddIssueViewModel(int idProject)
        {
            StatusList = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Selected = false, Text = IssueStatus.Open, Value = IssueStatus.Open},
                    new SelectListItem { Selected = false, Text = IssueStatus.Resolved, Value = IssueStatus.Resolved},
                }, "Value", "Text", 1);
            Deleted = false;
            IdProject = idProject;
            CreateAt = DateTime.Now;
           
        }
        public AddIssueViewModel( )
        {
            StatusList = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Selected = true, Text = "Open", Value = "0"},
                    new SelectListItem { Selected = false, Text = "Resolved", Value = "1"},
                }, "Value", "Text", 1);
            Deleted = false;
            
            CreateAt = DateTime.Now;

        }
    }
}