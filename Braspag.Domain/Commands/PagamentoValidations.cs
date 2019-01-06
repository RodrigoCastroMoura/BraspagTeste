using Braspag.Domain.DTO;
using Braspag.Domain.Interfaces.Repository;
using Braspag.Domain.Validators.Pagamento;
using FluentValidation;

namespace Braspag.Domain.Commands
{
   public static class PagamentoValidations
    {
       public static void ValidaCadastro(this PagamentoDto dto, IAdquirentesRepository iadquirentesRepository)
        {
            PagamentoValidators validator = new PagamentoValidators(iadquirentesRepository);
            validator.ValidateAndThrow(dto);
        }
    }
}
