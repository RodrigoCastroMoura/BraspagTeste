using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Braspag.Domain.DTO;



namespace Braspag.Domain.Filters
{
    public static class AdquirentesFilters
    {
        public static IMongoQuery[] CriarSpecification(this AdquirentesDto filtro)
        {
            List<IMongoQuery> criterio = new List<IMongoQuery>();

            if (!string.IsNullOrEmpty(filtro.adquirentes))
            {
                criterio.Add(Query<AdquirentesDto>.EQ(x => x.adquirentes, filtro.adquirentes));
            }


            return criterio.ToArray();
        }
    }
}
