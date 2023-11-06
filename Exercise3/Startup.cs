using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exercise3.Startup))]
namespace Exercise3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
