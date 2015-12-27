using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PUp.Startup))]
namespace PUp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
