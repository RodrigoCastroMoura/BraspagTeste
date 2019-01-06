using Braspag.Api.Mapper;
using System.Web.Http;


namespace Braspag.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AutoMapperConfig.RegisterMappings();

        }
    }
}
