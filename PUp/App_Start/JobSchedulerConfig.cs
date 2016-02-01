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
        public static void StartTaskEndingJobConfig()
        {
            // Grab the Scheduler instance from the Factory 
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            // and start it off
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<Jobs.TaskEndingJob>()
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
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<Jobs.ProjectDeadlineJob>()
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

        public static void StartAll()
        {
            StartTaskEndingJobConfig();
            StartProjectDeadLineJob();
        }
    }
}