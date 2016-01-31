using PUp.Models.Entity;
using PUp.Models.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.App_Start.Jobs
{
    public class TestJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Greetings from TestJob!");
            IssueRepository issueRepo = new IssueRepository();
            ProjectRepository prRepo = new ProjectRepository(issueRepo.DbContext);
            IssueEntity issue = new IssueEntity {
                Project= prRepo.FindById(1),
                RelatedArea = "From JobScheduler",
                Status = "Open",
                Description = "Issue form the Scheduler",
                AddAt= DateTime.Now,
                Deleted=false,
                EditAt= DateTime.Now,
                DeleteAt= DateTime.Now.AddDays(10)
            };
            
            issueRepo.Add(issue);
        }
    }
}