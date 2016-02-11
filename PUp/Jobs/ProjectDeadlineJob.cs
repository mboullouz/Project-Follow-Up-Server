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
    public class ProjectDeadlineJob : AbstractBaseJob
    {
        public override void Execute(IJobExecutionContext context)
        {
            base.Init();
            var projects = projectRepo.GetActive();
            foreach(var p in projects)
            {
                var now = DateTime.Now;
                if ((p.EndAt - now).TotalDays >= 0 && (p.EndAt - now).TotalDays <1 )
                {
                    foreach(var user in p.Contributors.ToList())
                    {
                      
                        NotificationEntity notif = new NotificationEntity();
                        notif.User = user;
                        notif.Message = "The project: " + p.Name + "Ends today, "
                            +p.Tasks.Where(t=>!t.Done).Count() + " Task(s) pending";
                        notif.Level = LevelFlag.DANGER;
                        notificationRepo.Add(notif);
                    }
                }
            }
        }
    }
}