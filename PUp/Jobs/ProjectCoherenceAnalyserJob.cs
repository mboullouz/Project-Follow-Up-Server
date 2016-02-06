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

        }

        public void NoTaskInTheProjectAfterCreatingIt()
        {
            var projects = projectRepo.GetAll().Where(p =>
                    p.AddAt <= DateTime.Now.AddMinutes(10)
                    && p.Tasks.Count <= 0
                    && p.EndAt >= DateTime.Now.AddHours(1)
                   ).ToList();
            var users = new List<UserEntity>();
            foreach (var p in projects)
            {
                p.Contributions.ToList().ForEach(c => users.Add(userRepo.FindById(c.UserId)));
                foreach (var u in users)
                {
                    NotificationEntity notif = new NotificationEntity();
                    notif.User = u;
                    notif.Message = "Warning! the project: " + p.Name + " does not contain any tasks ";
                    notificationRepo.Add(notif);
                }
            }


        }
    }
}