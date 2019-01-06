using Braspag.Domain.DTO;
using FluentValidation;
using Braspag.Domain.Interfaces.Repository;
using Braspag.Domain.Validators.Adquirentes;
using static Braspag.Domain.Validators.Adquirentes.AdquirentesValidators;

namespace Braspag.Domain.Commands
{
    public static class AdquirentesValidations
    {
        public static void ValidaCadastro(this AdquirentesDto dto, IAdquirentesRepository iadquirentesRepository)
        {
            AdquirentesValidators validator = new AdquirentesValidators(iadquirentesRepository);
            validator.ValidateAndThrow(dto);
        }

        public static void ValidaAlterar(this AdquirentesDto dto, IAdquirentesRepository iadquirentesRepository)
        {
            AdquirenteUpdateValidators validator = new AdquirenteUpdateValidators(iadquirentesRepository);

            validator.ValidateAndThrow(dto);
        }
    }
}
