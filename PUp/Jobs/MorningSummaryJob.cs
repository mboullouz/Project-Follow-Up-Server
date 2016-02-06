using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Jobs
{
    public class MorningSummaryJob : AbstractBaseJob
    {
        /// <summary>
        /// TODO: complete this..
        /// Send a summary every morining about activities, tasks ...
        /// </summary>
        /// <param name="context"></param>
        public override void Execute(IJobExecutionContext context)
        {
            base.Init();

        }
    }
}