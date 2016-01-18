using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUp.Models.Entity;

namespace PUp.Models.Repository
{
    public class ContributionRepository : IContributionRepository
    {

        private DatabaseContext dbContext;
        public ContributionRepository()
        {
            dbContext = new DatabaseContext(); 
        }

        public ContributionRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(ContributionEntity e)
        {
            dbContext.ContributionSet.Add(e);
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public ContributionEntity FindById(int id)
        {
            return dbContext.ContributionSet.SingleOrDefault();
        }

       
        public bool ContributionExists(ProjectEntity p, UserEntity u)
        {
           // int n=dbContext.ContributionSet.Where(c => c.ProjectId == p.Id && c.UserId == u.Id).ToList().Count();
            return GetByUserAndProject(u,p).Count>0;
        }

        public List<ContributionEntity> GetByProject(ProjectEntity project)
        {
            return dbContext.ContributionSet.Where(v=>v.Project.Id==project.Id).ToList();
        }

        public List<ContributionEntity> GetByUser(UserEntity user)
        {
            return dbContext.ContributionSet.Where(v => v.User.Id == user.Id).ToList();
        }

        public List<ContributionEntity> GetByUserAndProject(UserEntity user, ProjectEntity project)
        {
            return dbContext.ContributionSet.Where(v => v.User.Id == user.Id && v.Project.Id==project.Id).ToList();
        }

        public DatabaseContext GetDbContext()
        {
            return this.dbContext;
        }

        public void Remove(ContributionEntity e)
        {
            dbContext.ContributionSet.Remove(e);
            dbContext.SaveChanges();
        }

        public void SetDbContext(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<ContributionEntity> GetAll()
        {
           return dbContext.ContributionSet.ToList();
        }

        public void RemoveAllForUser(UserEntity user)
        {
            var contribs = dbContext.ContributionSet.Where(n => n.UserId == user.Id)
                .ToList();
            contribs.ForEach(n => dbContext.ContributionSet.Remove(n));
            dbContext.SaveChanges();
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
    }
}