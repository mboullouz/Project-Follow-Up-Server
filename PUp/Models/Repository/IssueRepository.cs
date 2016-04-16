﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUp.Models.Entity;

namespace PUp.Models.Repository
{
    public class IssueRepository : AbstractRepository<IssueEntity>
    {
        
        public IssueRepository() :base()
        {}

        public IssueRepository(DatabaseContext dbContext):base(dbContext)
        {}

        public override List<IssueEntity> GetAll()
        {
            return DbContext.IssueSet.Include("Project").Include("Submitter").ToList();
        }

        public override IssueEntity FindById(int id)
        {
            return DbContext.IssueSet.Where(i => i.Id == id).FirstOrDefault();
        }

        public List<IssueEntity> GetByProject(ProjectEntity p)
        {
            return DbContext.IssueSet.Where(i => i.Project.Id == p.Id).ToList();
        }
 
        public override void MarkDeleted(IssueEntity e)
        {
            throw new NotImplementedException();
        }

        internal IssueEntity MarkResolved(int id)
        {
            var issue = FindById(id); 
            issue.Status = IssueStatus.Resolved;
            DbContext.SaveChanges();
            return issue;
        }

        
        /// <summary>
        /// Flip the state of an issue
        /// </summary>
        /// <param name="id"></param>
        internal void OpenClose(int id)
        {
            var issue = FindById(id);
            if (issue.Status == IssueStatus.Open)
            {
                issue.Status = IssueStatus.Resolved;
              
            }
            else
            {
                issue.Status = IssueStatus.Open;
            }
           
            DbContext.SaveChanges();
        }
    }
}