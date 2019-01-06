using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Braspag.Domain.DTO;

namespace Braspag.Domain.Filters
{
    public static class UsuarioFilters
    {

        public static IMongoQuery[] CriarSpecification(this UsuarioDto filtro)
        {
            List<IMongoQuery> criterio = new List<IMongoQuery>();

            if (!string.IsNullOrEmpty(filtro.login))
            {
                criterio.Add(Query<UsuarioDto>.EQ(x => x.login, filtro.login));
            }

            if (!string.IsNullOrEmpty(filtro.senha))
            {
                criterio.Add(Query<UsuarioDto>.EQ(x => x.senha, filtro.senha));
            }


            return criterio.ToArray();
        }
    }
}
