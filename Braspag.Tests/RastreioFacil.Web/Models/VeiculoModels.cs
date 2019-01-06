using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RastreioFacil.Web.Models
{
    public class VeiculoModels
    {
        public string IMEI { get;  set; }

        public int? id_cliente { get;  set; }

        public int? id_marca { get;  set; }

        public int? id_rastreador { get;  set; }

        public int? id_status { get;  set; }

        public string ds_placa { get;  set; }

        public string ds_modelo { get;  set; }

        public int? nm_ano { get;  set; }

        public int? nm_modelo { get;  set; }

        public string nm_telefonico { get;  set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<DadosVeiculoModels> DadosVeiculo { get; set; }


        //public DateTime? dt_pagamento { get;  set; }

        //public DateTime? dt_vencimento { get;  set; }

        public bool? avisoBloqueio { get;  set; }

        public bool? comandoBloqueo { get;  set; }

        public bool? bloqueado { get;  set; }

        //public bool? comandoTempo { get;  set; }

        //public string tempoIgnicaoON { get;  set; }

        //public string tempoIgnicaoOFF { get;  set; }

        //public int? cd_user_cadm { get;  set; }

        //public DateTime? ts_user_cadm { get;  set; }

        //public int? cd_user_manu { get;  set; }

        //public DateTime? ts_user_manu { get;  set; }

    }
}