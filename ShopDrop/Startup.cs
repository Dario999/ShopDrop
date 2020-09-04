using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopDrop.Startup))]
namespace ShopDrop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
