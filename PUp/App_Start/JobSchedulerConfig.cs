using PUp.Jobs;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.App_Start
{
    public class JobSchedulerConfig
    {
        // Grab the Scheduler instance from the Factory 
        static IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
        public static void StartTaskEndingJobConfig()
        {
 
            IJobDetail job = JobBuilder.Create<TaskEndingJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(3599)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            scheduler.ScheduleJob(job, trigger);
        }

        public static void  StartProjectDeadLineJob()
        { 
            IJobDetail job = JobBuilder.Create<ProjectDeadlineJob>()
               .WithIdentity("ProjectDeadLineJob", "group2")
               .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger2", "group2")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(10) //Todo use:  .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(9, 30)) // execute job daily at 9:30
                    .RepeatForever())
                .Build();

           
            scheduler.ScheduleJob(job, trigger);
        }

        public static void StartProjectCoherenceAnalyzis()
        {
         
            IJobDetail job = JobBuilder.Create<ProjectCoherenceAnalyserJob>()
                .WithIdentity("job3", "group3")
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger3", "group3")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(150)
                    .RepeatForever())
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }

        public static void StartIssuesAnalysis()
        {

            IJobDetail job = JobBuilder.Create<IssuesAnalyzerJob>()
                .WithIdentity("job4", "group4")
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger4", "group4")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(2)
                    .RepeatForever())
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }

        public static void StartAll()
        {
            scheduler.Start();
            StartTaskEndingJobConfig();
            StartProjectDeadLineJob();
            StartProjectCoherenceAnalyzis();
            StartIssuesAnalysis();
        }
    }
}