using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PUp.Models;
using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

using System.Web.Security;

namespace PUp.App_Start
{
    /// <summary>
    /// Credit SO: see -> http://stackoverflow.com/a/19007440/2136116
    /// </summary>
    public class MyBasicAuth : AuthorizeAttribute 
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (Thread.CurrentPrincipal.Identity.Name.Length == 0)
            { // If an identity has not already been established by other means:
                AuthenticationHeaderValue auth = actionContext.Request.Headers.Authorization;
                if (string.Compare(auth.Scheme, "Basic", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    string credentials = UTF8Encoding.UTF8.GetString(Convert.FromBase64String(auth.Parameter));
                    int separatorIndex = credentials.IndexOf(':');
                    if (separatorIndex >= 0)
                    {
                        string userName = credentials.Substring(0, separatorIndex);
                        string password = credentials.Substring(separatorIndex + 1);
                        var UserManager = new UserManager<UserEntity>(new UserStore<UserEntity>(new DatabaseContext()));
                        var user = UserManager.Find(userName, password);
                        if (user != null)
                        {
                            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DatabaseContext()));
                            string[] roles = new string[user.Roles.ToList().Count];
                            var k = 0;
                            foreach (var r in user.Roles.ToList())
                            {
                                roles[k] = r.RoleId; k++;
                            }
                            Thread.CurrentPrincipal = actionContext.ControllerContext.RequestContext.Principal = new GenericPrincipal(new GenericIdentity(userName, "Basic"), roles);
                        }
                    }
                }
            }
            return base.IsAuthorized(actionContext);
        }
    }
}