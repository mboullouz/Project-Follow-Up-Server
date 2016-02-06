using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using PUp.Models.Entity;

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
            var users = new List<UserEntity>();
            foreach (var p in projects)
            {
                p.Contributions.ToList().ForEach(c => users.Add(userRepo.FindById(c.UserId)));
                string message = "Warning! the project: " + p.Name + " contains more than 3 issues "; ;
                NotifyAboutTooMuchIssues(users, message);
            }
        }

        private void NotifyAboutTooMuchIssues(List<UserEntity> users, string message)
        {
            foreach (var u in users)
            {
                NotificationEntity notif = new NotificationEntity();
                notif.User = u;
                notif.Message = message;
                notificationRepo.Add(notif);
            }
        }
    }
}