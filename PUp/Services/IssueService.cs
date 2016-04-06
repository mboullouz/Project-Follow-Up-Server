using PUp.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Services
{
    public class IssueService:BaseService
    { 
        public IssueService(string email, Models.ModelStateWrapper modelStateWrapper) : base(email, modelStateWrapper) { }

        public  IssueDto GetIssue(int id)
        {
            var issue = repo.IssueRepository.FindById(id);
            return new IssueDto(issue, 1);
        }
    }
}