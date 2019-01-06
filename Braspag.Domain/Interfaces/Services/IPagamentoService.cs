using Braspag.Domain.DTO;
using Braspag.Domain.Entities;

namespace Braspag.Domain.Interfaces.Services
{
    public interface IPagamentoService : System.IDisposable
    {
        Pagamento CadastrarPagamento(PagamentoDto dto);
    }
}
