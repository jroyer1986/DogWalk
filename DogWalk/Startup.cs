using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DogWalk.Startup))]
namespace DogWalk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
