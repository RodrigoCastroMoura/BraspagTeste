using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Braspag.Domain.DTO
{
    public class UsuarioDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get;  set; }

        public string login { get;  set; }

        public string senha { get;  set; }

    }
}
