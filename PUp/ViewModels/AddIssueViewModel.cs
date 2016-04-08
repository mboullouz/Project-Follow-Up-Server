using PUp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUp.ViewModels
{
    public class AddIssueViewModel
    {   

        /// <summary>
        /// Required and iniialized to 0 in case of new 
        /// </summary>
        [Required]
        public int Id { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be between {1} and {2} caracters.", MinimumLength = 15)]
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        public List<SimpleKeyValue<string,string>> StatusList { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateAt { get; set; }
        public string RelatedArea { get; set; }

        public AddIssueViewModel(int projectId)
        {
            ProjectId = projectId;
            Init();
        }
        public AddIssueViewModel( )
        {
            Init();
        }
        public void Init()
        {
            Id = 0;
            StatusList.Add(new SimpleKeyValue<string, string> { Key = "false", Value = IssueStatus.Open });
            StatusList.Add(new SimpleKeyValue<string, string> { Key = "true", Value = IssueStatus.Resolved });
            Deleted = false;
            CreateAt = DateTime.Now;
            Status = StatusList.FirstOrDefault().Key; //init 
        }
    }
}