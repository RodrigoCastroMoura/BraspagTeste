using Braspag.Domain.DTO;
using Braspag.Domain.Entities;
using System.Collections.Generic;

namespace Braspag.Domain.Interfaces.Repository
{
    public interface IAdquirentesRepository : IRepositoryBase<Adquirentes>
    {
        Adquirentes GetByAdquirentes(AdquirentesDto adquirentes);

        Adquirentes Cadastrar(AdquirentesDto dto);

        Adquirentes Alterar(AdquirentesDto dto);

        IEnumerable<Adquirentes> GetLista();

    }
}
