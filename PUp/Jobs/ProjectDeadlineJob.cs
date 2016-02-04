using PUp.Models.Entity;
using PUp.Models.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Jobs
{
    public class ProjectDeadlineJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            ProjectRepository prRepo = new ProjectRepository();
            NotificationRepository nfRepo = new NotificationRepository(prRepo.DbContext);
            UserRepository usRepo = new UserRepository(prRepo.DbContext);
            

            var projects = prRepo.GetActive();
            foreach(var p in projects)
            {
                var now = DateTime.Now;
                if ((p.EndAt - now).TotalDays >= 0 && (p.EndAt - now).TotalDays <1 )
                {
                    foreach(var contrib in p.Contributions)
                    {
                        var user = usRepo.FindById(contrib.UserId);
                        NotificationEntity notif = new NotificationEntity();
                        notif.User = user;
                        notif.Message = "Warning! the project: " + p.Name + "Ends today, "
                            +p.Tasks.Where(t=>!t.Done).Count() + " Task(s) pending";
                        nfRepo.Add(notif);
                    }
                }
            }
        }
    }
}