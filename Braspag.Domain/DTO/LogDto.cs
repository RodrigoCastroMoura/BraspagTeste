using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Braspag.Domain.DTO
{
    public class LogDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string json { get;  set; }

        public DateTime data { get;  set; }
    }
}
