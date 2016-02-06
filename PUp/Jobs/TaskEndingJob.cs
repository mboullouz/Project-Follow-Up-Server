using PUp.Models.Entity;
using PUp.Models.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Jobs
{
    public class TaskEndingJob : AbstractBaseJob
    {
        public override void Execute(IJobExecutionContext context)
        {
            base.Init();
            var projects = projectRepo.GetActive();
            foreach (var p in projects)
            {
                var now = DateTime.Now;
                foreach (var task in p.Tasks)
                {
                    if ( !task.Done && task.EstimatedTimeInMinutes<59)//1 hour before
                    {
                        var users = userRepo.GetAll();
                        foreach (var u in users)
                        {
                            NotificationEntity notif = new NotificationEntity();
                            notif.User = u;
                            notif.Message = "Action require attention! The task: " + task.Title
                                + " from the project: "+p.Name+" Ends today!"+ (task.keyFactor?" This task is a key factor":"");
                            notificationRepo.Add(notif);
                        }
                    }
                }
            }
        }
    }
}
