using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(imageUploadDemo.Startup))]
namespace imageUploadDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
