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
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
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
            StatusList.Add(new SimpleKeyValue<string, string> { Key = "false", Value = IssueStatus.Open });
            StatusList.Add(new SimpleKeyValue<string, string> { Key = "true", Value = IssueStatus.Resolved });
            Deleted = false;
            CreateAt = DateTime.Now;
        }
    }
}