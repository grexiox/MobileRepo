using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BackendMobileQueueService.Startup))]

namespace BackendMobileQueueService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}