using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Practices.Unity;
using WebApi.OutputCache.V2;
using AutoMapper;
using RastreioFacil.Library.Web;
using RastreioFacil.IoC;
using RastreioFacil.Domain.Interfaces.Services;
using RastreioFacil.Domain.Entities;
using RastreioFacil.Web.Models;
using RastreioFacil.Domain.DTO;


namespace RastreioFacil.Web.Controllers
{
    [RoutePrefix("V1/api/cliente")]
    public class ClienteApiController : ApiController 
    {
        private readonly IClienteServices service;

        public ClienteApiController(IClienteServices service)
        {
            this.service = service;
        }


        [HttpGet]
        [Route("VerificaToken")]
        [Authorize]
        [GzipCompression]
        [CacheOutput(ClientTimeSpan = 30, ServerTimeSpan = 30)]
        public async Task<HttpResponseMessage> VerificaToken()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                var claims = from c in identity.Claims select c;

                var dto = Mapper.Map<Cliente, ClienteDto>(service.GetCliente(Convert.ToInt32(SecurityDb.Decrypt(claims.ToList()[5].Value))));

                if (dto != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, false);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing && service != null)
            {
                service.Dispose();
            }

            base.Dispose(disposing);
        }

        [HttpGet]
        [Route("Listar")]
        [Authorize]
        [GzipCompression]
        [CacheOutput(ClientTimeSpan = 10, ServerTimeSpan = 10)]
        public async Task<HttpResponseMessage> GetCliente()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                var claims = from c in identity.Claims select c;

                var dto = Mapper.Map<Cliente, ClienteModels>(service.GetCliente(Convert.ToInt32(SecurityDb.Decrypt(claims.ToList()[5].Value))));

                return Request.CreateResponse(HttpStatusCode.OK, dto);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPut]
        [Route("TrocarSenha/{senha}/{confirm}")]
        [Authorize]
        public async Task<HttpResponseMessage> TrocarSenha(string senha,string confirm)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                var claims = from c in identity.Claims select c;

                var Senha = SecurityDb.Encrypt(senha);
                
                var Confirm = SecurityDb.Encrypt(confirm);

                if (Senha == Confirm)
                {
                    var dto = Mapper.Map<Cliente, ClienteDto>(service.GetCliente(Convert.ToInt32(SecurityDb.Decrypt(claims.ToList()[5].Value))));
                    
                    dto.ds_senha = Senha;
                    dto.fl_trocar_senha = false;

                    service.Alterar(dto);

                    return Request.CreateResponse(HttpStatusCode.OK, "Sua senha alterada com sucesso!");

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "As senhas não confere");
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("EnviarSenha/{cpf}")]   
        [AllowAnonymous]
        public async Task<HttpResponseMessage> EnviarSenha(string cpf)
        {
            try
            {
              
                string SenhaCaracteresValidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890@#&!?";
                int valormaximo = SenhaCaracteresValidos.Length;
                Random random = new Random(DateTime.Now.Millisecond);
                StringBuilder senha = new StringBuilder(6);
                for (int indice = 0; indice < 6; indice++)
                {
                    senha.Append(SenhaCaracteresValidos[random.Next(0, valormaximo)]);
                }

                var dto = Mapper.Map<Cliente, ClienteDto>(service.GetCliente(cpf));

                if (dto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Usuário não encontrado!");
                }

                dto.ds_senha = SecurityDb.Encrypt(senha.ToString());
                dto.fl_trocar_senha = true; ;

                service.Alterar(dto);

                var resoposta = Email.Send(dto.ds_nome, dto.ds_email, "Rastreio Fácil - Reenvio Senha ", "<p style='font-family: Arial, Helvetica; color: #000; font-size: 14px'>Olá <strong>" + dto.ds_nome + ",</strong><br /><br />Sua solicitação de reenvio de senha temporária foi feita com sucesso!<p style='font-family: Arial, Helvetica; color: #000; font-size: 13px;'>Favor acessar o sitema, digite o seu CPF e a nova senha,  que é: <strong>" + senha.ToString() + "<strong> </p><p style='font-family: Arial, Helvetica; color: black; font-size: 13px;'><strong>Favor não responder este e-mail!</strong></p>");

                if (resoposta != "Y")
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Estamos com  problema, tente mais tarde!");
                }

                return Request.CreateResponse(HttpStatusCode.OK, "Sua senha foi enviada para seu e-mail com sucesso!");
          
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }





    }
}
