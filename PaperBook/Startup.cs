using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaperBook.Startup))]
namespace PaperBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
