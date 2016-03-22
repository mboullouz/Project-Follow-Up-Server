using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.SimpleObject
{
    public class IssueDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? EditAt { get; set; }
        public string RelatedArea { get; set; }
        public ProjectDto Project { set; get; }
        public DateTime AddAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public UserDto Submitter { get; set; }

        public IssueDto(IssueEntity issue,int depth=5)
        {
            Init(issue,--depth);
        }

        public void Init(IssueEntity issue, int depth)
        {
            if (issue != null && depth>0)
            {
                Id = issue.Id;
                Description = issue.Description;
                Status = issue.Status;
                Deleted = issue.Deleted;
                EditAt = issue.EditAt;
                RelatedArea = issue.RelatedArea;
                Project = new ProjectDto(issue.Project,depth);
                AddAt = issue.AddAt;
                DeleteAt = issue.DeleteAt;
                Submitter = new UserDto(issue.Submitter,depth);
            }
        }
    }
}