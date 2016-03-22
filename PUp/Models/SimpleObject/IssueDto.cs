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
    }
}