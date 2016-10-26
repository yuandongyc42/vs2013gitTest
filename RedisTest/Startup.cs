using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RedisTest.Startup))]
namespace RedisTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
