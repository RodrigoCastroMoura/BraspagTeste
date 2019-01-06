using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Braspag.Domain.DTO
{
    public class PagamentoDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get;  set; }

        public string comprador { get; set; }

        public string numeroCartao { get; set; }

        public string dataVencimento { get; set; }

        public string codSegurancaCartao { get; set; }

        public decimal? valorCompra { get; set; }

        public decimal? valorLojista { get; set; }

        public decimal? valorAdquirente { get; set; }

        public string adquirente { get;  set; }

        public string bandeira { get;  set; }

        public DateTime data { get; set; }
    }
}
