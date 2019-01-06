using Braspag.Domain.DTO;
using Braspag.Domain.Entities;

namespace Braspag.Domain.Interfaces.Repository
{
    public interface IPagamentoRepository : IRepositoryBase<Pagamento>
    {
        Pagamento CadastrarPagamento(PagamentoDto dto);
    }
}
