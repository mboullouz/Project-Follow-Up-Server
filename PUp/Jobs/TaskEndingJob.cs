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
    public class TaskEndingJob : AbstractBaseJob
    {   

        /// <summary>
        /// This job must notify only in the working hours interval 
        /// See AppConst
        /// TODO : Create More Generic Base Classes that run specialy in the interval!
        /// </summary>
        /// <param name="context"></param>
        public override void Execute(IJobExecutionContext context)
        {
            base.Init();
            int currentHour = DateTime.Now.Hour;
            if(currentHour<AppConst.DayStart-2 || currentHour > AppConst.DayEnd)
            {
                return;
            }
            var projects = projectRepo.GetActive();
            foreach (var p in projects)
            {
                var now = DateTime.Now;
                var tasks = p.Tasks.Where(t =>
                               t.Deleted == false 
                            && t.Done == false 
                            && t.EndAt!=null 
                            && t.EndAt.GetValueOrDefault().Hour>=AppConst.DayEnd-3);
                foreach (var task in tasks)
                {
                    if ( !task.Done)//1 hour before && task.EstimatedTimeInMinutes<59
                    {
                        var users = p.Contributors;
                        foreach (var u in users)
                        {
                            NotificationEntity notif = new NotificationEntity();
                            notif.User = u;
                            notif.Message = "The task: <" + task.Title+ "> from the project: <"+p.Name+"> Ends today in less than 3 Hours!"+ (task.KeyFactor?" This task is a key factor":"");
                            notif.Level = LevelFlag.Danger;
                            notificationRepo.Add(notif);
                        }
                    }
                }
            }
        }
    }
}
