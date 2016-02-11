using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Jobs
{
    /// <summary>
    /// Define some metrics and warn users about abnormalies and problems
    /// Eg: 
    ///     Too much tasks in a project
    ///     Tasks start to be added near project deadlines
    ///     Too small objective or description text for the project
    /// </summary>
    public class ProjectCoherenceAnalyserJob : AbstractBaseJob
    {
        public override void Execute(IJobExecutionContext context)
        {
            base.Init();
            NoTaskInTheProjectAfterCreatingIt();
            TasksCanBeDeletedOrPostponed();
        }

        public void TasksCanBeDeletedOrPostponed()
        {
            var projects = projectRepo.GetAll().Where(p =>
                    p.AddAt <= DateTime.Now.AddMinutes(10)
                    && p.Tasks.Count > 0
                    && p.EndAt >= DateTime.Now.AddHours(1)
                   ).ToList();
            foreach (var p in projects)
            {
                var tasks =p.Tasks.ToList().Where(t =>!t.Urgent && !t.Critical).ToList();
                foreach (var t in tasks)
                {
                    NotificationEntity notif = new NotificationEntity();
                    notif.User = t.Executor;
                    notif.Message = " The task: " + t.Title+ 
                                    " from the project: "+p.Name+" can be postponed";
                    notif.Level = LevelFlag.WARNING;
                    notificationRepo.Add(notif);
                }
            }
        }

        public void NoTaskInTheProjectAfterCreatingIt()
        {
            var projects = projectRepo.GetAll().Where(p =>
                    p.AddAt <= DateTime.Now.AddMinutes(10)
                    && p.Tasks.Count <= 0
                    && p.EndAt >= DateTime.Now.AddHours(1)
                   ).ToList();
            
            foreach (var p in projects)
            {
               
                foreach (var u in p.Contributors.ToList())
                {
                    NotificationEntity notif = new NotificationEntity();
                    notif.User = u;
                    notif.Message = "The project: " + p.Name + " does not contain any tasks ";
                    notif.Level = LevelFlag.DANGER;
                    notificationRepo.Add(notif);

                }
            }
        }
    }
}