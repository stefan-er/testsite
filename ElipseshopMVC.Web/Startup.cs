using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElipseshopMVC.Web.Startup))]
namespace ElipseshopMVC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
