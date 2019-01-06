using System.Collections;
using System.Collections.Generic;
using Braspag.Library;

namespace Braspag.Domain.InterFaces.Repository
{
    public interface IRepositoryBaseOnly<TEntity> where TEntity : class
    {
        long Count();

        void Dispose();

        bool Exists(object id);

        TEntity First();

        TEntity First(MongoDB.Driver.IMongoQuery query);

        System.Collections.Generic.IEnumerable<TEntity> GetAll(MongoDB.Driver.IMongoQuery query);

        System.Collections.Generic.IEnumerable<TEntity> GetAll();

        TEntity GetByID(string id);
    }
}
