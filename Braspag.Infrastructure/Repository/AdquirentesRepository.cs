using Braspag.Domain.DTO;
using Braspag.Domain.Entities;
using Braspag.Domain.Interfaces.Context;
using Braspag.Domain.Interfaces.Repository;
using Braspag.Domain.Filters;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Braspag.Domain.Update;
using System.Collections.Generic;

namespace Braspag.Infrastructure.Repository
{
    public class AdquirentesRepository : RepositoryBase<Adquirentes>, IAdquirentesRepository
    {
        public AdquirentesRepository(IDataContext context)
           : base(context)
        {
        }

        public Adquirentes Cadastrar(AdquirentesDto dto)
        {
            var adquirentes = Adquirentes.RetornaAdquerente(dto);

            Save(adquirentes);

            return adquirentes;
        }

        public Adquirentes GetByAdquirentes(AdquirentesDto dto)
        {
            IMongoQuery query = Query.And(dto.CriarSpecification());

            return First(query);
        }

        public IEnumerable<Adquirentes> GetLista()
        {
            return GetAll();
        }

        public Adquirentes Alterar(AdquirentesDto dto)
        {
            var adquirente = Adquirentes.RetornaAdquerente(dto);

            IMongoUpdate update = adquirente.UpDateAdquirentes();

            var query = Query<Adquirentes>.EQ(x => x._id, adquirente._id);

            Update(query, update);

            return adquirente;

        }

    }
}
