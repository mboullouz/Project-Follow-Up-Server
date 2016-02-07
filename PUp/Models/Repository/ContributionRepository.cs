using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUp.Models.Entity;

namespace PUp.Models.Repository
{
    public class ContributionRepository : AbstractRepository<ContributionEntity>
    {

        
        public ContributionRepository():base()
        {
        }

        public ContributionRepository(DatabaseContext dbContext):base(dbContext)
        {
        }

       
 

        public override ContributionEntity FindById(int id)
        {
            return DbContext.ContributionSet.SingleOrDefault();
        }

       
        public bool ContributionExists(ProjectEntity p, UserEntity u)
        {
           // int n=dbContext.ContributionSet.Where(c => c.ProjectId == p.Id && c.UserId == u.Id).ToList().Count();
            return GetByUserAndProject(u,p).Count>0;
        }

        public List<ContributionEntity> GetByProject(ProjectEntity project)
        {
            return DbContext.ContributionSet.Where(v=>v.Project.Id==project.Id).ToList();
        }

        public List<ContributionEntity> GetByUser(UserEntity user)
        {
            return DbContext.ContributionSet.Where(v => v.User.Id == user.Id).ToList();
        }

        public List<ContributionEntity> GetByUserAndProject(UserEntity user, ProjectEntity project)
        {
            return DbContext.ContributionSet.Where(v => v.User.Id == user.Id && v.Project.Id==project.Id).ToList();
        }
 
  

        public void RemoveAllForUser(UserEntity user)
        {
            var contribs = DbContext.ContributionSet.Where(n => n.UserId == user.Id)
                .ToList();
            contribs.ForEach(n => DbContext.ContributionSet.Remove(n));
            DbContext.SaveChanges();
        }

        public HashSet<UserEntity> UsersByProject(ProjectEntity p)
        {
            var contribs = p.Contributions;
            HashSet<UserEntity> users = new HashSet<UserEntity>();
            foreach(var c in contribs)
            {
                users.Add(c.User);
            }
            return users;
        }

        public void AddContributionIfNotExists(ProjectEntity project, UserEntity user, TaskEntity task)
        {
            if (!ContributionExists(project, user))
            {
                var contrib = new ContributionEntity
                {
                    AddAt = DateTime.Now,
                    EndAt = task.Project.EndAt,
                    ProjectId = project.Id,
                    UserId = user.Id,
                    User=user,
                    Project=project,
                    Role = "AddTask"
                };
                Add(contrib);
                project.Contributions.Add(contrib);
                user.Contributions.Add(contrib);
            }
        }

        public void AddContributionIfNotExists(ProjectEntity project, UserEntity user, IssueEntity issue)
        {
            if (!ContributionExists(project, user))
            {
                var contrib = new ContributionEntity
                {
                    AddAt = DateTime.Now,
                    EndAt = project.EndAt,
                    ProjectId = project.Id,
                    User = user,
                    Project = project,
                    UserId = user.Id,
                    Role =  ContributionEntity.RolesToString(3)
                };
                Add(contrib);
                project.Contributions.Add(contrib);
                user.Contributions.Add(contrib);
            }
        }

        public override void MarkDeleted(ContributionEntity e)
        {
            throw new NotImplementedException();
        }
    }
}