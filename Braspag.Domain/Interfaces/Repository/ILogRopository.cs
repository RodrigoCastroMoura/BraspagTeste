using Braspag.Domain.DTO;
using Braspag.Domain.Entities;

namespace Braspag.Domain.Interfaces.Repository
{
    public interface ILogRopository : IRepositoryBase<Log>
    {
        Log Cadastrar(LogDto dto);
    }
}
