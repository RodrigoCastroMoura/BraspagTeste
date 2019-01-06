using Braspag.Domain.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Braspag.Domain.Entities
{
    public class Adquirentes
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; private set; }

        public string adquirentes { get; private set; }

        public decimal? visa { get; private set; }
        
        public decimal? master { get; private set; }

        public decimal? elo { get; private set; }

        protected Adquirentes()
        {

        }

        private Adquirentes(AdquirentesDto adquirente)
        {
            this._id = adquirente._id;
            this.adquirentes = adquirente.adquirentes;
            this.visa = adquirente.visa;
            this.master = adquirente.master;
            this.elo = adquirente.elo;
        }

        public static Adquirentes RetornaAdquerente(AdquirentesDto adquirentesDto)
        {
            return new Adquirentes(adquirentesDto);
        }

    }
}
