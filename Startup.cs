using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VK_Analyze.Startup))]
namespace VK_Analyze
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);
        }
    }
}
