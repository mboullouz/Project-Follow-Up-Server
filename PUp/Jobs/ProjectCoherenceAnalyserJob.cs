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
    public class ProjectCoherenceAnalyserJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            ProjectRepository prRepo = new ProjectRepository();
            NotificationRepository nfRepo = new NotificationRepository(prRepo.DbContext);
            UserRepository usRepo = new UserRepository(prRepo.DbContext);
        }
    }
}