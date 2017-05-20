using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GrupuriDeStudiuIP35.Startup))]
namespace GrupuriDeStudiuIP35
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
