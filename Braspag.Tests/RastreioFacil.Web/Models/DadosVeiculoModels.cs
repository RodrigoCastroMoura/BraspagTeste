using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RastreioFacil.Web.Models
{
    public class DadosVeiculoModels
    {
        public int? id_dados { get; set; }
        public string IMEI { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string altidude { get; set; }
        public string speed { get; set; }
        public bool? ignicao { get; set; }
        public DateTime? data { get; set; }
        public string dataDevice { get; set; }

       // public virtual RastreadorDto Rastreador { get; set; }
    }
}