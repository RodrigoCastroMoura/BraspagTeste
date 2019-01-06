using Microsoft.Practices.Unity;
using Braspag.Service;
using Braspag.Domain.Interfaces.Services;


namespace Braspag.IoC.Services
{
    public static class ServiceDependencyResolver
    {
        public static void RegisterDependencies(UnityContainer container)
        {
            container.RegisterType<IAdiquirentesServices, AdquirentesAppService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPagamentoService, PagamentoAppService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILogServices, LogAppService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUsuarioServices, UsuarioAppSevice>(new HierarchicalLifetimeManager());

        }
    }
}
