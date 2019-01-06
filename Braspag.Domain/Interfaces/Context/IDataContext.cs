using System;
using MongoDB.Driver;


namespace Braspag.Domain.Interfaces.Context
{
    public interface IDataContext :IDisposable
    {
        MongoDatabase Context { get; }
        void RequestDone();
        void RequestStart();
    }
}
