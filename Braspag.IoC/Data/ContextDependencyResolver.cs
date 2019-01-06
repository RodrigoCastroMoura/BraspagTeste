using Microsoft.Practices.Unity;
using Braspag.Domain.Interfaces;
using Braspag.Domain.Interfaces.Context;
using Braspag.Domain.Interfaces.Services;
using Braspag.Domain.Interfaces.UnityOfWork;
using Braspag.Domain.InterFaces.Repository;
using Braspag.Infrastructure.Context;
using Braspag.Infrastructure.Repository;
using Braspag.Infrastructure.UnityOfWork;

namespace Braspag.IoC.Data
{
    public static class ContextDependencyResolver
    {
        public static void RegisterDependencies(UnityContainer container)
        {
           
            container.RegisterType<IUnitiOfWork, UnitiOfWork>(new HierarchicalLifetimeManager());

            container.RegisterType<IDataContext, DataContext>(new HierarchicalLifetimeManager());

        }
    }
}
