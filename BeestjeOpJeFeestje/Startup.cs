using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeestjeOpJeFeestje.Startup))]
namespace BeestjeOpJeFeestje
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
