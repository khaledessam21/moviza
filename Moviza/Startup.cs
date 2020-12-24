using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Moviza.Startup))]
namespace Moviza
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
