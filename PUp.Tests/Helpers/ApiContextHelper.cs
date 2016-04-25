using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PUp.Controllers;
using PUp.Models;
using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PUp.Tests.Helpers
{
    class ApiContextHelper
    {  
        public static UserEntity CurrentUser
        {
            get
            {
                return GetContextCurrentUser();
            }
        }
       public static void MockApiControllerRequest(ApiController apiController)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DatabaseContext()));
            string[] roles = new string[ CurrentUser.Roles.ToList().Count];
            var k = 0;
            foreach (var r in  CurrentUser.Roles.ToList())
            {
                roles[k] = r.RoleId; k++;
            }
            apiController.RequestContext.Principal = new GenericPrincipal(new GenericIdentity(CurrentUser.Email, "Basic"), roles);
            apiController.Request = new HttpRequestMessage();
        }

        public static UserEntity GetContextCurrentUser()
        {
            string userName = "m@boullouz.com";
            string password = "123*Aa";
            var UserManager = new UserManager<UserEntity>(new UserStore<UserEntity>(new DatabaseContext()));
            var user = UserManager.Find(userName, password);
            return user;
        }
    }
}
