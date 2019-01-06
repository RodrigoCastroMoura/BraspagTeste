using System;
using Braspag.Domain.Interfaces.UnityOfWork;
using Braspag.Domain.Interfaces.Context;

namespace Braspag.Infrastructure.UnityOfWork
{
    public class UnitiOfWork : IUnitiOfWork
    {
        protected readonly IDataContext context;

        public UnitiOfWork(IDataContext context)
        {
            this.context = context;
        }
    }
}
