using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(N_tier_solution.Startup))]
namespace N_tier_solution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // tell our app we want to use signalr
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
