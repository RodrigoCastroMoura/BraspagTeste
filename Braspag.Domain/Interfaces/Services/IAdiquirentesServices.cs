using Braspag.Domain.Entities;
using System.Collections.Generic;

namespace Braspag.Domain.Interfaces.Services
{
    public interface IAdiquirentesServices : System.IDisposable
    {
        Adquirentes GetByAdquirentes(DTO.AdquirentesDto dto);

        Adquirentes Cadastrar(DTO.AdquirentesDto adquirente);

        Adquirentes Alterar(DTO.AdquirentesDto dto);

        IEnumerable<Adquirentes> GetLista();

    }
}

