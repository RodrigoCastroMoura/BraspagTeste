using Braspag.Domain.DTO;
using Braspag.Domain.Interfaces.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Braspag.Domain.Validators.Adquirentes
{
    public class AdquirenteUpdateValidators : AbstractValidator<AdquirentesDto>
    {

        private readonly IAdquirentesRepository iadquirentesRepository;

        public AdquirenteUpdateValidators(IAdquirentesRepository iadquirentesRepository)
        {
            this.iadquirentesRepository = iadquirentesRepository;

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
