using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Braspag.Domain.DTO
{
    public class AdquirentesDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; private set; }

        public string adquirentes { get; set; }

        public decimal? visa { get; set; }

        public decimal? master { get; set; }
        
        public decimal? elo { get; set; }
    }
}
