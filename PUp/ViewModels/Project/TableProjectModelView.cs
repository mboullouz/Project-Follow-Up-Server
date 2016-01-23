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
        public List<ContributionEntity> UserContributions { get; set; }
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
            foreach(var cts in p.Contributions)
            {
                System.Diagnostics.Debug.WriteLine(" Contrib: Userid " + cts.UserId + " ProjID: "+cts.ProjectId);
                if (cts.UserId == CurrentUser.Id)
                {
                    System.Diagnostics.Debug.WriteLine(" TRUE :)");
                        return true;
                }
                    
            }
            return false;
        }
        public List<Contributor> GetContributorsTo(ProjectEntity p)
        {    
            //TODO move this to a repository
            Models.DatabaseContext dbContext = new Models.DatabaseContext();
            var contribs = dbContext.ContributionSet.Where(v => v.Project.Id ==p.Id);
            var res = from u in dbContext.Users
                      join c in dbContext.ContributionSet on u.Id equals c.UserId
                      join pr in dbContext.ProjectSet on  c.ProjectId equals pr.Id 
                      where   pr.Id == p.Id
                      select new Contributor
                      {
                          Email = u.Email,
                          
                      };
                      

            return res.ToList();
        }
        public List<ContributionEntity> FindContributionByProject(ProjectEntity project)
        {
            IContributionRepository repo = new ContributionRepository();
            return repo.GetAll().ToList();
        }
    }

    public class Contributor
    {
        public String Email  { get; set; }
    }
}