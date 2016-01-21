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
        public List<UserEntity> GetContributorsTo(ProjectEntity p)
        {
            IContributionRepository contribRepo = new ContributionRepository();
            IUserRepository userRepo = new UserRepository();
            var users = new List<UserEntity>();
            var contribs = contribRepo.GetByProject(p);
            var allUsers = userRepo.GetAll();
            //TODO remove this and replace it all!
            foreach(var c in contribs)
            {
                foreach (var u in allUsers)
                {
                    if (c.UserId == u.Id)
                    {
                        users.Add(u);
                    }
                }
            }
            return users;
        }
        public List<ContributionEntity> FindContributionByProject(ProjectEntity project)
        {
            IContributionRepository repo = new ContributionRepository();
            return repo.GetAll().ToList();
        }
    }
}