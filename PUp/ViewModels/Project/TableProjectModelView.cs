using PUp.Models.Entity;
using PUp.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.ViewModels.Project
{
    public class TableProjectModelView
    {
        public List<ProjectEntity> Projects { get; set; }
        public List<ProjectEntity> OtherProjects { get; set; }
       
        public UserEntity CurrentUser { get; set; }

        public int CountDone(ProjectEntity p)
        {
            return p.Tasks.Where(t => t.Done == true).Count();
        }
        public bool IsFinish(ProjectEntity p)
        {
            if (p == null || p.EndAt == null)
                return false;
            if (p.EndAt <= DateTime.Now)
            {
                return true;
            }
             
            return false;
        }
        public bool ImContributingTo(ProjectEntity p)
        {
            foreach(var cts in p.Contributors)
            {
                
                if (cts.Id == CurrentUser.Id)
                {
                   
                        return true;
                }
                    
            }
            return false;
        }
        public List<UserEntity> GetContributorsTo(ProjectEntity p)
        {    
            //TODO move this to a repository
            Models.DatabaseContext dbContext = new Models.DatabaseContext();
            return p.Contributors.ToList();
        }
       
    }

    public class Contributor
    {
        public String Email  { get; set; }
    }
}