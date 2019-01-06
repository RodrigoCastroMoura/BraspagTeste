using Braspag.Domain.DTO;
using Braspag.Domain.Entities;
using Braspag.Domain.Interfaces.Context;
using Braspag.Domain.Interfaces.Repository;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Braspag.Infrastructure.Repository
{
    public class LogRepository : RepositoryBase<Log>, ILogRopository
    {
        public LogRepository(IDataContext context)
       : base(context)
        {
        }

        public Log Cadastrar(LogDto dto)
        {
            var log = Log.RetornaLog(dto);

            Save(log);

            return log;
        }
    }
}
