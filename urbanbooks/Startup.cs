using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(urbanbooks.Startup))]
namespace urbanbooks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
