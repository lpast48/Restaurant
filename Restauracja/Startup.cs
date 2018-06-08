using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Restauracja.Startup))]
namespace Restauracja
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
