using PUp.Models.Entity;
using PUp.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels.Project
{
    public class ProjectTimelineViewModel
    {
        public ProjectEntity Project { get; set; }
        public List<TaskEntity> Tasks;
         
        public List<object> Elements { get; set; }
        public List<NotificationEntity> Notifs { get; set; }
        private List<object> AllElements = new List<object>();
        public ProjectTimelineViewModel(ProjectEntity p)
        {
            Elements = new List<object>();
            Project = p;
            Notifs = new List<NotificationEntity>();
            NotificationRepository notifRepository = new NotificationRepository();
            Notifs = notifRepository.GetAll();
           Tasks = p.Tasks.ToList();
            Init();
        }
        public void Init()
        {
            Tasks.ForEach(t => AllElements.Add(t));
            Notifs.ForEach(n => AllElements.Add(n));
            AllElements  =AllElements.OrderByDescending(ob => ((IBasicEntity)ob).AddAt).ToList() ;
             
            AllElements.ForEach(obj => Elements.Add(obj));
        }
    }

   
}