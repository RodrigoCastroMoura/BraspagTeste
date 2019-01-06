using Braspag.Domain.Commands;
using Braspag.Domain.DTO;
using Braspag.Domain.Entities;
using Braspag.Domain.Interfaces.Repository;
using Braspag.Domain.Interfaces.Services;
using Braspag.Domain.Interfaces.UnityOfWork;
using Braspag.Domain.Service;

namespace Braspag.Service
{
    public class PagamentoAppService : AppService, IPagamentoService
    {

        private readonly IPagamentoRepository ipagamentoRepository;
        private readonly IAdquirentesRepository iadquirentesRepository;

        public PagamentoAppService(
            IUnitiOfWork unitOfWork, 
            IPagamentoRepository ipagamentoRepository,
            IAdquirentesRepository iadquirentesRepository)
            : base(unitOfWork)
        {
            this.ipagamentoRepository = ipagamentoRepository;
            this.iadquirentesRepository = iadquirentesRepository;

        }

        public Pagamento CadastrarPagamento(PagamentoDto dto)
        {
            dto.ValidaCadastro(iadquirentesRepository);


            var dtoAdquirentes = new AdquirentesDto()
            {
                adquirentes = dto.adquirente
            };

            var adquirentes = iadquirentesRepository.GetByAdquirentes(dtoAdquirentes);

            decimal? valor = 0;

            switch (dto.bandeira)
            {
                case "visa":
                    valor = (adquirentes.visa / 100) * dto.valorCompra;
                    break;
                    case "master":
                    valor = (adquirentes.master / 100) * dto.valorCompra;
                    break;
                    case "elo":
                    valor = (adquirentes.elo / 100) * dto.valorCompra;
                    break;
                default:
                    throw new System.Exception("Bandeira não encontrada.");

            }

            dto.valorAdquirente = valor;
            dto.valorLojista = dto.valorCompra - valor;
            

          
            var pagamento = ipagamentoRepository.CadastrarPagamento(dto);

            return pagamento;

        }

    }
}
