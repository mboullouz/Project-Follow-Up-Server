using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web;
using Moq;
using System.Security.Principal;
using PUp.Models.Entity;
using PUp.Models.Repository;

namespace PUp.Tests.Helpers 
{
    class ContextHelper
    {
        public static HttpContext FakeContext()
        {
            HttpContext.Current = new HttpContext(
            new HttpRequest("", "http://localhost:64634/", ""),
            new HttpResponse(new System.IO.StringWriter())
            );
            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity("a@b.com"),
                new string[] { "ADMIN" }
            );
            return HttpContext.Current;
        }

        public static UserEntity CurrentUserEntity()
        {
            var context = FakeContext();
            IUserRepository uRep = new UserRepository();
            return uRep.FindByEmail(context.User.Identity.Name);
        }
    }
}
