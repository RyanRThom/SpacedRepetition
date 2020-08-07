using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpacedRepetition.Startup))]
namespace SpacedRepetition
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
