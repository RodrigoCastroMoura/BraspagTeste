using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Braspag.Domain.Interfaces.Context;
using Braspag.Domain.InterFaces.Repository;
using MongoDB.Bson;


namespace Braspag.Infrastructure.Repository 
{
    public class RepositoryBaseOnly<TEntity> : IDisposable, IRepositoryBaseOnly<TEntity> where TEntity : class
    {

        internal IDataContext Db;
        internal MongoCollection dbSet;

        public RepositoryBaseOnly(IDataContext Db)
        {
            this.Db = Db;
            dbSet = Db.Context.GetCollection(typeof(TEntity).Name);
        }

        public TEntity GetByID(string id)
        {
            return dbSet.FindOneByIdAs<TEntity>(ObjectId.Parse(id));
        }

        public TEntity First(MongoDB.Driver.IMongoQuery query)
        {
            return dbSet.FindOneAs<TEntity>(query);
        }

        public TEntity First()
        {
            return dbSet.FindOneAs<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.FindAllAs<TEntity>();
        }

        public IEnumerable<TEntity> GetAll(IMongoQuery query)
        {
            return dbSet.FindAs<TEntity>(query);
        }

        public bool Exists(object id)
        {
            return dbSet.FindOneByIdAs<TEntity>(id.ToString()) != null;
        }

        public long Count()
        {
            return dbSet.Count();
        }

       
        public virtual void Dispose()
        {

        }     

    }
}
