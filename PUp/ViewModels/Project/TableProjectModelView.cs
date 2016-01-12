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
            if (p.EndAt <= DateTime.Now)
            {
                return true;
            }
             
            return false;
        }
        public bool ImContributingTo(ProjectEntity p)
        {
            if (CurrentUser == null || p ==null || p.Contributions ==null)
                return false;
            foreach(var ctr in p.Contributions)
            {
                if (ctr == null || ctr.User==null)
                    return false;
                  if (ctr.User.Id == CurrentUser.Id)
                    return true;
            }
            return false;
        }
        public List<UserEntity> GetContributorsTo(ProjectEntity p)
        {
           
            List<UserEntity> users = new List<UserEntity>();
            FindContributionByProject(p).Where(c=>c.Project!=null && c.User!=null).ToList().ForEach(c=>users.Add(c.User));
            return users;
        }
        public List<ContributionEntity> FindContributionByProject(ProjectEntity project)
        {
            IContributionRepository repo = new ContributionRepository();
            return repo.GetAll().ToList();
        }
    }
}