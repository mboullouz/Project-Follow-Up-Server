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
        private List<TaskDto> Tasks;
        private List<object> Elements { get; set; }
        private List<NotificationDto> Notifs { get; set; }
        private List<IssueDto> Issues { get; set; }
        private List<object> AllElements = new List<object>();

        //Elements to expose
        public List<object> TodayElements { get; set; }
        public List<object> YesterdayElements { get; set; }
        public List<object> OneWeekElements { get; set; }

        public TimelineViewModel(ProjectEntity p)
        {
            Elements = new List<object>();
            Project = new ProjectDto(p);
            Notifs = new List<NotificationDto>();
            Issues = p.Issues.ToList().ToDto();
            NotificationRepository notifRepository = new NotificationRepository();
            Notifs = notifRepository.GetAll().OrderByDescending(n=>n.AddAt).Take(10).ToList().ToDto();//Todo Remove this
            Tasks = p.Tasks.ToList().ToDto();
            Init();
            InitTodayElements();
            InitYesterdayElements();
            InitOneWeekElements();
        }
        public void Init()
        {
            Tasks.ForEach(t => AllElements.Add(t));
            Notifs.ForEach(n => AllElements.Add(n));
            Issues.ForEach(i => AllElements.Add(i));
            AllElements  =AllElements.OrderByDescending(ob => ((IBasicEntity)ob).AddAt).ToList() ;

            AllElements.ForEach(obj => Elements.Add(obj));
        }
        public void InitTodayElements()
        {
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59
            TodayElements= new List<object>();
            TodayElements = Elements.Where(ob => ((IBasicEntity)ob).AddAt >= startDateTime && ((IBasicEntity)ob).AddAt < endDateTime).ToList();
        }

        public void InitYesterdayElements()
        {
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            startDateTime = startDateTime.AddDays(-1); //Yesterday at 00:00:00
            DateTime endDateTime = DateTime.Today.AddTicks(-1); //Yesterday at 23:59:59
            YesterdayElements = new List<object>();
            YesterdayElements = Elements.Where(ob => ((IBasicEntity)ob).AddAt >= startDateTime && ((IBasicEntity)ob).AddAt < endDateTime).ToList();
        }

        public void InitOneWeekElements()
        {
            DateTime yesterday = DateTime.Today; //Today at 00:00:00
            yesterday = yesterday.AddDays(-1); //Yesterday at 00:00:00
            DateTime endDateTime = yesterday.AddTicks(-1); //Yesterday-1 day at 23:59:59
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            startDateTime = startDateTime.AddDays(-8); //Yesterday at 00:00:00

            OneWeekElements = new List<object>();
            OneWeekElements = Elements.Where(ob => ((IBasicEntity)ob).AddAt >= startDateTime && ((IBasicEntity)ob).AddAt < endDateTime).ToList();
        }
    }

   
}