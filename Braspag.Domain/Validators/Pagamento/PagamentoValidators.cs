using FluentValidation;
using Braspag.Domain.DTO;
using Braspag.Domain.Interfaces.Repository;


namespace Braspag.Domain.Validators.Pagamento
{
    public class PagamentoValidators : AbstractValidator<PagamentoDto>
    {
        private readonly IAdquirentesRepository iadquirentesRepository;

        public PagamentoValidators(IAdquirentesRepository iadquirentesRepository)
        {

            this.iadquirentesRepository = iadquirentesRepository;

            RuleFor(pagamento => pagamento.comprador)
                .NotEmpty()
                .NotNull()
                .WithMessage("Comprador não informado");

            RuleFor(pagamento => pagamento.numeroCartao)
                .NotEmpty()
                .NotNull()
                .WithMessage("Numero do Cartão não informado");

            RuleFor(pagamento => pagamento.dataVencimento)
                .NotEmpty()
                .NotNull()
                .WithMessage("Data de vencimento não informado");

            RuleFor(pagamento => pagamento.codSegurancaCartao)
                .NotEmpty()
                .NotNull()
                .WithMessage("Codigo de seguraça não informado");

            RuleFor(pagamento => pagamento.valorCompra)
                .NotEmpty()
                .NotNull()
                .WithMessage("Valor da compra não informado");

            RuleFor(pagamento => pagamento.adquirente)
               .NotEmpty()
               .NotNull();
               //.Must(VerificaAdquirente).WithMessage("Adquirente não encontrado")
               //.WithMessage("Adquirente não informado");
                
            RuleFor(pagamento => pagamento.bandeira)
              .NotEmpty()
              .NotNull()
              .WithMessage("Bandeira não informado");

        }

        private bool VerificaAdquirente(string adquirentes)
        {
            var dto = new AdquirentesDto()
            {
                adquirentes = adquirentes
            };

            var obj = this.iadquirentesRepository.GetByAdquirentes(dto);

            if (obj != null)
                return true;
            else
                return false;
        }


    }
}
