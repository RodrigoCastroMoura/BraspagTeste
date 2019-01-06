using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Braspag.Domain.Interfaces.Context;
using Braspag.Domain.Interfaces.Repository;
using MongoDB.Bson;

namespace Braspag.Infrastructure.Repository
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {

        internal IDataContext Db;
        internal MongoCollection dbSet;

        public RepositoryBase(IDataContext Db)
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

        public virtual void Save(TEntity entity)
        {
            dbSet.Save(entity);  
        }

        public virtual void Update(IMongoQuery query, IMongoUpdate update)
        {
            
            dbSet.Update(query,update);
        }

        //public virtual void SaveAll(IEnumerable<TEntity> items)
        //{
        //    items.ToList().ForEach(Save);
        //}

        public void Delete(object id)
        {
            dbSet.Remove(Query.EQ("_id", id.ToString()));
        }

        public void DeleteAll()
        {
            dbSet.RemoveAll();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public long Count()
        {
            return dbSet.Count();
        }

        public long Count(MongoDB.Driver.IMongoQuery query)
        {

            return dbSet.FindAs<TEntity>(query).Count();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.RequestDone();
                GC.SuppressFinalize(this);
            }
        }
    }
}
