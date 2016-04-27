using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Routing;
using PUp.Tests.Helpers;

namespace PUp.Tests.GeneralTest
{
    [TestClass]
    public class GlobalTest
    {
        [TestMethod]
        public void Test_global_router_config()
        {
            RouteData routeData = RouterDataHelper.DefineForUrl("~/Home/Index/1");
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual("1", routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_test_current_user_not_null_build_simple_context()
        {
            var context = ContextHelper.FakeContext();          
            Assert.AreEqual("med@med.com", context.User.Identity.Name);
            Assert.AreEqual(true, context.User.IsInRole("ADMIN"));
            Assert.IsTrue(context.User.Identity.IsAuthenticated);
        }

        [TestMethod]
        public void Test_user_entity_from_current_context_not_null()
        {   
            Assert.IsNotNull(ContextHelper.CurrentUserEntity()); 
            Assert.AreEqual("med@med.com", ContextHelper.CurrentUserEntity().Email);
        }
       
    }
}
