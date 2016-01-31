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
        public static void StartSimpleConfig()
        {
            // Grab the Scheduler instance from the Factory 
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            // and start it off
            scheduler.Start();
            Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };
            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<Jobs.TestJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            scheduler.ScheduleJob(job, trigger);
        }
    }
}