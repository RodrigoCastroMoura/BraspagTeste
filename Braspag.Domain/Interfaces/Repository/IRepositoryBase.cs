using System.Collections;
using System.Collections.Generic;
using Braspag.Library;



namespace Braspag.Domain.Interfaces.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        long Count();

        long Count(MongoDB.Driver.IMongoQuery query);

        void Delete(object id);

        void DeleteAll();

        void Dispose();

        bool Exists(object id);

        TEntity First();

        TEntity First(MongoDB.Driver.IMongoQuery query);

        System.Collections.Generic.IEnumerable<TEntity> GetAll(MongoDB.Driver.IMongoQuery query);

        System.Collections.Generic.IEnumerable<TEntity> GetAll();

        void Save(TEntity entity);

        //void SaveAll(System.Collections.Generic.IEnumerable<TEntity> items);

        TEntity GetByID(string id);
       
    }
        
}
