using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_Speaker.Startup))]
namespace Project_Speaker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
