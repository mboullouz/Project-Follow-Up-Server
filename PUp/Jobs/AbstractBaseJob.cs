using PUp.Models;
using PUp.Models.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Jobs
{   
        /// <summary>
        /// TODO: complete this..
        ///  Set a base for jobs
        /// </summary>
        /// <param name="context"></param>
    public abstract class AbstractBaseJob : IJob
    {
      protected ProjectRepository projectRepo;
      protected NotificationRepository notificationRepo;
      protected UserRepository userRepo;
      protected DatabaseContext dbContext = new DatabaseContext();
      
      public virtual void Init(){
        projectRepo = new ProjectRepository(dbContext);
        notificationRepo = new NotificationRepository(dbContext);
        userRepo = new UserRepository(dbContext);
        
      }

      public abstract void Execute(IJobExecutionContext context);
    }
    
}
