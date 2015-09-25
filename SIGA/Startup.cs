using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SIGA.Startup))]
namespace SIGA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
