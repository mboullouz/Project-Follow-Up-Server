using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using PUp.Models.Entity;
using PUp.Models;

namespace PUp.Jobs
{
    public class IssuesAnalyzerJob : AbstractBaseJob
    {
        public override void Execute(IJobExecutionContext context)
        {
            base.Init();
            var projects = projectRepo.GetAll().Where(p =>
                   p.AddAt <= DateTime.Now.AddMinutes(10)
                   && p.Issues.Count >= 3
                   && p.EndAt >= DateTime.Now.AddHours(1)
                  ).ToList();
            
            foreach (var p in projects)
            {
                
                string message = "The project: " + p.Name + " contains more than 3 issues "; ;
                NotifyAboutTooMuchIssues(p.Contributors.ToList(), message);
            }
        }

        private void NotifyAboutTooMuchIssues(List<UserEntity> users, string message)
        {
            foreach (var u in users)
            {
                NotificationEntity notif = new NotificationEntity();
                notif.User = u;
                notif.Message = message;
                notif.Level = LevelFlag.WARNING;
                notificationRepo.Add(notif);
            }
        }
    }
}