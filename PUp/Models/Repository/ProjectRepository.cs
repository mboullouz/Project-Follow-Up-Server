﻿using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PUp.Models.Repository
{   
    public class ProjectRepository : IProjectRepository
    {
        private DatabaseContext dbContext;
        public ProjectRepository()
        {
            dbContext = new DatabaseContext();
        }
        public void SetDbContext(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(ProjectEntity e)
        {
           dbContext.ProjectSet.Add(e);
            dbContext.SaveChanges();
        }
        public List<ProjectEntity> GetByUser(UserEntity user)
        {  
            List<ProjectEntity> projects = new List<ProjectEntity>();
            //TODO remove this and handle error before getting there!
            if (user == null)
            {
                return new List<ProjectEntity>();
            }
            //TODO do it in one query !
            List<ContributionEntity> contributions = dbContext.ContributionSet.Where(c => c.User.Id == user.Id).ToList();
            var query = (from c in contributions
                         join p in  dbContext.ProjectSet
                         on c.Project.Id  equals p.Id 
                         into grouping                       
                         select new { grouping }).ToList();           
             query.ForEach(p=> p.grouping.ToList().ForEach(k=>projects.Add(k)));
            return projects;

        }
        public ProjectEntity FindById(int id)
        {
            return dbContext.ProjectSet.SingleOrDefault(e => e.Id == id);
        }

        public List<ProjectEntity> GetAll()
        {
            return dbContext.ProjectSet.ToList();
        }

        public void Remove(ProjectEntity e)
        {
            dbContext.ProjectSet.Remove(e);
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public DatabaseContext GetDbContext()
        {
            return this.dbContext;
        }
    }
}