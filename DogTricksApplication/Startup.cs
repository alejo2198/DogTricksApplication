using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DogTricksApplication.Startup))]
namespace DogTricksApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
