using Braspag.Domain.DTO;
using Braspag.Domain.Entities;
using Braspag.Domain.Interfaces.Context;
using Braspag.Domain.Interfaces.Repository;

namespace Braspag.Infrastructure.Repository
{
    public class PagamentoRepository : RepositoryBase<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(IDataContext context)
           : base(context)
        {

        }

        public Pagamento CadastrarPagamento(PagamentoDto dto)
        {
            var pagamento = Pagamento.RetornaPagamento(dto);

            Save(pagamento);

            return pagamento;
        }

    }
}
