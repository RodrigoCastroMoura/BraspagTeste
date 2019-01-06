using System.Configuration;
using MongoDB.Driver;
using Braspag.Domain.Interfaces.Context;




namespace Braspag.Infrastructure.Context
{
    public class DataContext : IDataContext
    {   
         private readonly MongoDatabase MongoDatabase;

         private MongoServer Server;

         public  MongoDatabase Context
         {
            get
            {
                return this.MongoDatabase;
            }
         }

        public DataContext()
        {
            //MongoUrl url = new MongoUrl(ConfigurationManager.ConnectionStrings["conexaoMongoDB"].ConnectionString);
            MongoUrl url = new MongoUrl("mongodb://braspagteste:uyt9Cb5kD2Fh1Br4sPFcyXBHjGG0IiQNgLrwu9OrKTxCuNn4DUjIDtvgMO3FsJykwz9QrNywV0dfH5ItEgAnOw==@braspagteste.documents.azure.com:10255/?ssl=true&replicaSet=globaldb");
            MongoClient client = new MongoClient(url);
  
            Server = client.GetServer();
            this.MongoDatabase = Server.GetDatabase("Braspag");
        }

        public void RequestStart()
        {
            //Server.RequestStart(this.MongoDatabase);
        }

        public void RequestDone()
        {
           // Server.RequestDone();

        }

        public void Dispose()
        {

        }

    }
}
