using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASUPWebApplication.Startup))]
namespace ASUPWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
