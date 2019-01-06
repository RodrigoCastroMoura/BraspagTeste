using Braspag.Domain.DTO;
using Braspag.Domain.Entities;
using Braspag.Domain.Interfaces.Context;
using Braspag.Domain.Interfaces.Repository;
using Braspag.Domain.Filters;
using MongoDB.Driver;
using MongoDB.Driver.Builders;


namespace Braspag.Infrastructure.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IDataContext context) : base(context)
        {

        }

        public Usuario Autenticacao(UsuarioDto dto)
        {
            IMongoQuery query = Query.And(dto.CriarSpecification());

            return First(query);
        }
    }
}
