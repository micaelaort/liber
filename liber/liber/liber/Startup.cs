using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(liber.Startup))]
namespace liber
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
