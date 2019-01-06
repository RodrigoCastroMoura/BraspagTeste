using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoletoNet;
using System.Web.Mvc;
using Boleto = BoletoNet.Boleto;

namespace RastreioFacil.Web.Models
{
    public class Boleto
    {
        public Boleto(int Banco)
        {
            boletoBancario = new BoletoBancario();
            boletoBancario.CodigoBanco = (short)Banco;
        }

        public BoletoBancario boletoBancario { get; set; }


        public byte[] BancodoBrasilPDF(ClienteModels dto)
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            #region Exemplo Carteira 16, com nosso número de 11 posições
            /*
         * Nesse exemplo utilizamos a carteira 16 e o nosso número no máximo de 11 posições.
         * Não é necessário informar o numero do convênio e nem o tipo da modalidade.
         * O nosso número tem que ter no máximo 11 posições.
         */

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.01m, "16", "09876543210", c);

            #endregion Exemplo Carteira 16, com nosso número de 11 posições

            #region Exemplo Carteira 16, convênio de 6 posições e tipo modalidade 21
            /*
         * Nesse exemplo utilizamos a carteira 16 e o número do convênio de 6 posições.
         * É obrigatório informar o tipo da modalidade 21.
         * O nosso número tem que ter no máximo 10 posições.
         */

            //Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
            //c.Convenio = 123456;

            //BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.01, "16", "09876543210", c);
            //b.TipoModalidade = "21";
            #endregion Exemplo Carteira 16, convênio de 6 posições e tipo modalidade 21

            #region Exemplo Carteira 18, com nosso número de 11 posições
            /*
         * Nesse exemplo utilizamos a carteira 18 e o nosso número no máximo de 11 posições.
         * Não é necessário informar o numero do convênio e nem o tipo da modalidade.
         * O nosso número tem que ter no máximo 11 posições.
         */

            //Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
            //BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.01, "18", "09876543210", c);

            #endregion Exemplo Carteira 18, com nosso número de 11 posições

            #region Exemplo Carteira 18, convênio de 6 posições e tipo modalidade 21
            /*
         * Nesse exemplo utilizamos a carteira 18 e o número do convênio de 6 posições.
         * É obrigatório informar o tipo da modalidade 21.
         * O nosso número tem que ter no máximo 10 posições.
         */

            //Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
            //c.Convenio = 123456;
            //BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.01, "18", "09876543210", c);
            //b.TipoModalidade = "21";
            #endregion Exemplo Carteira 18, convênio de 6 posições e tipo modalidade 21


            b.NumeroDocumento = "12415487";

            b.Sacado = new Sacado(dto.ds_cpf , dto.ds_nome + " " + dto.ds_sobre_nome);
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            //Adiciona as instruções ao boleto
            #region Instruções
            //Protestar
            Instrucao_BancoBrasil item = new Instrucao_BancoBrasil(9, 5);
            b.Instrucoes.Add(item);
            //ImportanciaporDiaDesconto
            item = new Instrucao_BancoBrasil(30, 0);
            b.Instrucoes.Add(item);
            //ProtestarAposNDiasCorridos
            item = new Instrucao_BancoBrasil(81, 15);
            b.Instrucoes.Add(item);
            #endregion Instruções

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            boletoBancario.RemoveSimboloMoedaValorDocumento = true;
            boletoBancario.AjustaTamanhoFonte(12, 10, 14, 14);


            return boletoBancario.MontaBytesPDF();
        }

    }
}