using System;
using Braspag.Domain.Interfaces.UnityOfWork;

namespace Braspag.Domain.Service
{
    public abstract class AppService :IDisposable 
    {
        private readonly IUnitiOfWork unitOfWork;

        public AppService(IUnitiOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            
        }
    }
}
