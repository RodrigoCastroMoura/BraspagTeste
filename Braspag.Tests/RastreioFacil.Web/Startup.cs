using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using RastreioFacil.Library.Web;
using RastreioFacil.Web.Formatter;
using Microsoft.Practices.Unity;
using RastreioFacil.IoC;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;


namespace RastreioFacil.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            // Enable Cors
            app.UseCors(CorsOptions.AllowAll);

            // Web API configuration and services
            httpConfiguration.Formatters.Clear();
            httpConfiguration.Formatters.Insert(0, new JilFormatter());


            //GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector), new ApiVersioningSelector(GlobalConfiguration.Configuration));
            //GlobalConfiguration.Configuration.EnsureInitialized();


            var container = new UnityContainer();
            IoC.IoC.Resolve(container);
            httpConfiguration.DependencyResolver = new UnityResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));


            ConfigureOAuth(app);
            WebApiConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/bearerToken"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(10),
                Provider = new AuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(oAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
            
    }

    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}