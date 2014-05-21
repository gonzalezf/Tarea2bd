using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(foro2.Startup))]
namespace foro2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
