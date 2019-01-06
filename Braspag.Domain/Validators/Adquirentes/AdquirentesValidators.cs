using FluentValidation;
using Braspag.Domain.DTO;
using Braspag.Domain.Interfaces.Repository;

namespace Braspag.Domain.Validators.Adquirentes
{
    public class AdquirentesValidators : AbstractValidator<AdquirentesDto>
    {
        private readonly IAdquirentesRepository iadquirentesRepository;

        public AdquirentesValidators(IAdquirentesRepository iadquirentesRepository)
        {
            this.iadquirentesRepository = iadquirentesRepository;

            RuleFor(adquirentes => adquirentes.adquirentes)
               .NotNull()
               .NotEmpty()
               .WithMessage("Adquirente não informado.")
               .Length(3, 50).WithMessage("Adquirente deve ter ente 3 a 50 caracter.")
               .Must(VerificaAdquirente).WithMessage("Adquirente já Cadastrado.");

            RuleFor(adquirentes => adquirentes.visa)
                .NotEmpty()
                .NotNull()
                .Must(VerificaMenorQueZero)
                .WithMessage("Taxa da Visa não informada");

            RuleFor(adquirentes => adquirentes.master)
                .NotEmpty()
                .NotNull()
                .Must(VerificaMenorQueZero)
                .WithMessage("Taxa da Master não informada");

            RuleFor(adquirentes => adquirentes.elo)
                .NotEmpty()
                .NotNull()
                .Must(VerificaMenorQueZero)
                .WithMessage("Taxa da Elo não informada");

        }

        private bool VerificaAdquirente(string adquirentes)
        {
            var dto = new AdquirentesDto()
            {
                adquirentes = adquirentes
            };

            var obj = this.iadquirentesRepository.GetByAdquirentes(dto);

            if (obj == null)
                return true;
            else
                return false;
        }


        public bool VerificaMenorQueZero(decimal? id)
        {
            if (id == null)
            {
                return false;
            }
            else
            {
                if (id <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

    }
}
