using Braspag.Domain.Entities;
using Braspag.Domain.DTO;

namespace Braspag.Domain.Interfaces.Services
{
    public interface ILogServices : System.IDisposable
    {
        Log Cadastrar(LogDto dto);
    }
}
