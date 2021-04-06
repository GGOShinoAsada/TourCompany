using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TourCompany.Startup))]
namespace TourCompany
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
