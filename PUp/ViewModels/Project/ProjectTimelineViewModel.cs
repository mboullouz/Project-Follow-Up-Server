using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels.Project
{
    public class ProjectTimelineViewModel
    {
        public ProjectEntity project { get; set; }
        public List<TaskEntity> tasks
        {
            get
            {
                return project.Tasks.OrderByDescending(t => t.EditAt).ToList();
            }
            set
            {
                tasks=value;
            }
        }
        public Stack<object> elements { get; set; }
        public List<NotificationEntity> notifs { get; set; }
        public List<object> allElements { get; set; }
        public ProjectTimelineViewModel()
        {

        }
        public void Init()
        {
            tasks.ForEach(t => allElements.Add(t));
            notifs.ForEach(n => allElements.Add(n));
             
        }
    }

   
}