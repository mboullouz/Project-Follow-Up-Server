using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Services
{
    public class DashboardService: BaseService
    {
        public DashboardService(string email, Models.ModelStateWrapper modelStateWrapper) : base(email, modelStateWrapper) { }
    }
}