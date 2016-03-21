using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.SimpleObject
{
    public class ProjectView
    {
        public ProjectEntity Project { get; set; }
        public int Progress { get; set; }
        public int TasksDone { get; set; }
        public int TotalTasks { get; set; }
        public bool Over { get; set; }

        public ProjectView(ProjectEntity p)
        {
            Project = p;
            Init();
        }
        public void Init()
        {
            TotalTasks = Project.Tasks.Count();
            TasksDone = Project.Tasks.Where(t => t.Done == true).Count();
            Progress = (int)(TasksDone / (TotalTasks + 0.1) * 100);
            Over = Project.EndAt < DateTime.Now;
        }
    }
}