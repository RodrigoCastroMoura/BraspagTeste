using Braspag.Domain.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Braspag.Domain.Entities
{
    public class Pagamento
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; private set; }

        public string comprador { get; private set; }

        public string numeroCartao { get; private set; }

        public string dataVencimento { get; private set; }

        public string codSegurancaCartao { get; private set; }

        public decimal? valorCompra { get; private set; }

        public decimal? valorLojista { get; private set; }

        public decimal? valorAdquirente { get; private set; }

        public string adquirente { get; private set; }

        public string bandeira { get; private set; }

        public DateTime data { get; private set; }

        protected Pagamento()
        {

        }

        private Pagamento(PagamentoDto dto)
        {
            this._id = this._id;
            this.comprador = dto.comprador;
            this.numeroCartao = dto.numeroCartao;
            this.dataVencimento = dto.dataVencimento;
            this.codSegurancaCartao = dto.codSegurancaCartao;
            this.valorCompra = dto.valorCompra;
            this.valorLojista = dto.valorLojista;
            this.valorAdquirente = dto.valorAdquirente;
            this.adquirente = dto.adquirente;
            this.bandeira = dto.bandeira;
            this.data = DateTime.Now;
        }

        public static Pagamento RetornaPagamento(PagamentoDto dto)
        {
            return new Pagamento(dto);
        }

    }
}
