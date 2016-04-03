using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Services
{
    public class IssueService:BaseService
    { 
        public IssueService(string email, Models.ModelStateWrapper modelStateWrapper) : base(email, modelStateWrapper) { }
    }
}