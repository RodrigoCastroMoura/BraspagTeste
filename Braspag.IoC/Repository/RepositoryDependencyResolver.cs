using Microsoft.Practices.Unity;
using Braspag.Domain.Interfaces.Repository;
using Braspag.Infrastructure.Repository;


namespace Braspag.IoC.Repository
{
    public static class RepositoryDependencyResolver
    {
        public static void RegisterDependencies(UnityContainer container)
        {
            container.RegisterType<IAdquirentesRepository, AdquirentesRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPagamentoRepository, PagamentoRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ILogRopository, LogRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUsuarioRepository, UsuarioRepository>(new HierarchicalLifetimeManager());

        }
    }
}
