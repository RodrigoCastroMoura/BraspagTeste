using Braspag.Domain.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Braspag.Domain.Entities
{
    public class Log
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; private set; }

        public string json { get; private set; }

        public DateTime data { get; private set; }


        protected Log()
        {

        }

        private Log(LogDto dto)
        {
            this._id = dto._id;
            this.json = dto.json;
            this.data = dto.data; 
        }

        public static Log RetornaLog(LogDto dto)
        {
            return new Log(dto);
        }
    }

}
