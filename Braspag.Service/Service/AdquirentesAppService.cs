using Braspag.Domain.Commands;
using Braspag.Domain.DTO;
using Braspag.Domain.Entities;
using Braspag.Domain.Interfaces.Repository;
using Braspag.Domain.Interfaces.Services;
using Braspag.Domain.Interfaces.UnityOfWork;
using Braspag.Domain.Service;
using System.Collections.Generic;

namespace Braspag.Service
{
    public class AdquirentesAppService : AppService, IAdiquirentesServices
    {
        private readonly IAdquirentesRepository iadquirentesRepository;

        public AdquirentesAppService(
            IUnitiOfWork unitOfWork,
            IAdquirentesRepository iadquirentesRepository)
            : base(unitOfWork)
        {
            this.iadquirentesRepository = iadquirentesRepository;

        }

        public Adquirentes Cadastrar(AdquirentesDto dto)
        {
            dto.ValidaCadastro(iadquirentesRepository);

            var adquirente = iadquirentesRepository.Cadastrar(dto);

            return adquirente;
        }


        public IEnumerable<Adquirentes> GetLista()
        {
            return iadquirentesRepository.GetLista();
        }

        public Adquirentes GetByAdquirentes(AdquirentesDto dto)
        {
            return iadquirentesRepository.GetByAdquirentes(dto);
        }


        public Adquirentes Alterar(AdquirentesDto dto)
        {

            dto.ValidaAlterar(this.iadquirentesRepository);

            var adquirente = iadquirentesRepository.Alterar(dto);

            return adquirente;

        }
    }
}
