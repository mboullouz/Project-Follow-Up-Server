using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Services
{
    public class IssueService:BaseService
    { 
        public IssueService(string s, System.Web.Http.ModelBinding.ModelStateDictionary modelState) : base(s,modelState) { }
    }
}