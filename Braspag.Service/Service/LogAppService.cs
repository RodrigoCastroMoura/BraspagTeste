using Braspag.Domain.DTO;
using Braspag.Domain.Entities;
using Braspag.Domain.Interfaces.Repository;
using Braspag.Domain.Interfaces.Services;
using Braspag.Domain.Interfaces.UnityOfWork;
using Braspag.Domain.Service;

namespace Braspag.Service
{
    public class LogAppService : AppService, ILogServices
    {
        private readonly ILogRopository ilogRopository;

        public LogAppService(IUnitiOfWork unitOfWork,
               ILogRopository ilogRopository 
            ) : base(unitOfWork)
        {
            this.ilogRopository = ilogRopository;

        }

        public Log Cadastrar(LogDto dto)
        {
            var log = ilogRopository.Cadastrar(dto);

            return log;
        }
    }
}
