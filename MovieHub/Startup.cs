using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieHub.Startup))]
namespace MovieHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
