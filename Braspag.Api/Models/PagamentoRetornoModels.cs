using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Braspag.Api.Models
{
    public class PagamentoRetornoModels
    {
        public string comprador { get; set; }

        public decimal valorCompra { get; set; }

        public decimal valorLojista { get; set; }

        public decimal valorAdquirente { get; set; }

        public string adquirente { get; set; }

        public string bandeira { get; set; }

        public DateTime data { get; set; }
    }
}