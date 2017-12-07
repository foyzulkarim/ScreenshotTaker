using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScreenTakerServer.Startup))]
namespace ScreenTakerServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
