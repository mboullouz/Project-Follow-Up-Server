using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using PUp.Models.Entity;
using PUp.Models;

namespace PUp.Jobs
{   
    /// <summary>
    /// Analyze issues and keep user informed
    /// </summary>
    public class IssuesAnalyzerJob : AbstractBaseJob
    {
        public override void Execute(IJobExecutionContext context)
        {
            base.Init();
            TooMuchIssues();
            MoreIssuesThanTheTasks();
           
        }

        public void TooMuchIssues()
        {
            // after 10min and 1 hour before the end of project if still 3 open issues
            var projects = projectRepo.GetAll().Where(p =>
                   p.AddAt <= DateTime.Now.AddMinutes(10)
                   && p.EndAt >= DateTime.Now.AddHours(1)
                   && p.Deleted == false
                   && p.Issues.Where(i => i.Status == IssueStatus.Open && i.Deleted == false).ToList().Count >= 3
                  ).ToList();

            foreach (var p in projects)
            {
                string message = "The project: " + p.Name + " contains more than 3 issues ";
                notificationRepo.NotifyAllUserInProject(p, message);
            }
        }

        public void MoreIssuesThanTheTasks()
        {
            var projects = projectRepo.GetAll().Where(p =>
                   p.AddAt <= DateTime.Now.AddMinutes(10)
                   && p.EndAt >= DateTime.Now.AddHours(1)
                   && p.Deleted == false
                   &&
                      p.Issues.Where(i => i.Status == IssueStatus.Open && i.Deleted == false).ToList().Count
                      >=
                      p.Tasks.Where(t => t.Deleted == false && t.Done == false).ToList().Count
                  ).ToList();
            foreach (var p in projects)
            {
                string message = "The project: <" + p.Name + "> contains more issues than tasks! ";
                notificationRepo.NotifyAllUserInProject(p, message,LevelFlag.Danger);
            }
        }
    }
}