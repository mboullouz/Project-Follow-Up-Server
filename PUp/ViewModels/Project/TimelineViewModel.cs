using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.Models.SimpleObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels.Project
{
    public class TimelineViewModel:BaseModelView
    {
        public ProjectDto Project { get; set; }
        public List<TaskDto> Tasks;
         
        public List<object> Elements { get; set; }
        public List<NotificationDto> Notifs { get; set; }
        public List<IssueDto> Issues { get; set; }
        private List<object> AllElements = new List<object>();
        public TimelineViewModel(ProjectEntity p)
        {
            Elements = new List<object>();
            Project = new ProjectDto(p,1);
            Notifs = new List<NotificationDto>();
            Issues = p.Issues.ToList().ToDto();
            NotificationRepository notifRepository = new NotificationRepository();
            Notifs = notifRepository.GetAll().ToDto();//Todo Remove this
            Tasks = p.Tasks.ToList().ToDto();
            Init();
        }
        public void Init()
        {
            Tasks.ForEach(t => AllElements.Add(t));
            Notifs.ForEach(n => AllElements.Add(n));
            Issues.ForEach(i => AllElements.Add(i));
            AllElements  =AllElements.OrderByDescending(ob => ((IBasicEntity)ob).AddAt).ToList() ;

            AllElements.ForEach(obj => Elements.Add(obj));
        }
    }

   
}