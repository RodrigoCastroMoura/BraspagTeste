

using Braspag.Domain.DTO;
using Braspag.Domain.Entities;

namespace Braspag.Domain.Interfaces.Services
{
    public interface IUsuarioServices :System.IDisposable
    {
        Usuario Autenticacao(string login, string senha);
    }
}
