﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.SimpleObject
{
    public class ProjectDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Finish { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? EditAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public DateTime StartAt { get; set; }
        public UserDto Owner { get; set; }
        public ICollection<UserDto> Contributors { get; set; }
        public DateTime EndAt { get; set; }
        public string Benifite { get; set; }
        public string Objective { get; set; }
        public virtual ICollection<TaskDto> Tasks { get; set; }
        public virtual ICollection<IssueDto> Issues { get; set; }
        public DateTime? DeleteAt { get; set; }
        public DateTime AddAt { get; set; }
    }
}