using PUp.Models.Entity;
using PUp.Models.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Jobs
{
    public class TaskEndingJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            ProjectRepository prRepo = new ProjectRepository();
            NotificationRepository nfRepo = new NotificationRepository(prRepo.DbContext);
            UserRepository usRepo = new UserRepository(prRepo.DbContext);
            var projects = prRepo.GetActive();
            foreach (var p in projects)
            {
                var now = DateTime.Now;
                foreach (var task in p.Tasks)
                {
                    if ( !task.Done && task.EstimatedTimeInMinutes<59)//1 hour before
                    {
                        var users = usRepo.GetAll();
                        foreach (var u in users)
                        {
                            NotificationEntity notif = new NotificationEntity();
                            notif.User = u;
                            notif.Message = "Action require attention! The task: " + task.Title + " from the project: "+p.Name+" Ends today !";
                            nfRepo.Add(notif);
                        }
                    }
                }
            }
        }
    }
}
