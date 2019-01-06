using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RastreioFacil.Web.Models
{
    public class ClienteModels
    {
        //public int id_cliente { get; private set; }

        public string hash { get; private set; }

        public string ds_nome { get; private set; }

        public string ds_sobre_nome { get; private set; }

        public string ds_cpf { get; private set; }

        public DateTime? dt_nascimento { get; private set; }

        public string sexo { get; private set; }

        public string nm_cep { get; private set; }

        public string ds_numero { get; private set; }

        public string ds_complemento { get; private set; }

        public string ds_telefone { get; private set; }

        public string ds_celular { get; private set; }

        public string ds_email { get; private set; }

        //public string ds_senha { get; private set; }

        //public bool? fl_ativo { get; private set; }

        //public bool? fl_trocar_senha { get; private set; }

        //public int? cd_user_cadm { get; private set; }

        //public DateTime? ts_user_cadm { get; private set; }

        //public int? cd_user_manu { get; private set; }

        //public DateTime? ts_user_manu { get; private set; }

        public virtual ICollection<VeiculoModels> Veiculo { get; private set; }

    }
}