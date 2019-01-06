using System;
using Microsoft.Practices.Unity;
using Braspag.IoC.Data;
using Braspag.IoC.Repository;
using Braspag.IoC.Services;


namespace Braspag.IoC
{
    public static class IoC
    {
        public static void Resolve(UnityContainer container)
        {
            ServiceDependencyResolver.RegisterDependencies(container);
            RepositoryDependencyResolver.RegisterDependencies(container);
            ContextDependencyResolver.RegisterDependencies(container);         
        }
    }
}
