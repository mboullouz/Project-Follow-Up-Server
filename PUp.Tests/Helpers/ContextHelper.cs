using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web;
using Moq;
using System.Security.Principal;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.Models;

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
                new GenericIdentity("med@med.com"),
                new string[] { "ADMIN" }
            );
            return HttpContext.Current;
        }

        public static UserEntity CurrentUserEntity()
        {
            var context = FakeContext();
            UserRepository uRep = new UserRepository();
            return uRep.FindByEmail(context.User.Identity.Name);
        }

        /// <summary>
        /// Some operation may need the same database context
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static UserEntity CurrentUserEntity(DatabaseContext dbContext)
        {
            var context = FakeContext();
            UserRepository uRep = new UserRepository(dbContext);
            return uRep.FindByEmail(context.User.Identity.Name);
        }

        public static void InitControllerContext(Controller controller)
        {
            var context = FakeContext();
            Mock<ControllerContext> controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(p => p.HttpContext.Request.Browser.Browser).Returns("1");
            controller.ControllerContext = controllerContext.Object;
            var httpCxt = controller.ControllerContext.HttpContext;
            httpCxt.User = context.User;
        }
    }
}
