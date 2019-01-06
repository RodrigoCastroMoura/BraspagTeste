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
using RastreioFacil.Service;
using RastreioFacil.Domain.Interfaces.Services;
using RastreioFacil.Domain.Entities;
using RastreioFacil.Web.Models;
using RastreioFacil.Domain.DTO;
namespace RastreioFacil.Web.Controllers.V1
{
    [RoutePrefix("V1/api/veiculo")]
    public class VeiculoApiController : ApiController
    {
        private readonly IVeiculoServices iVeiculoServices;

        public VeiculoApiController(IVeiculoServices iVeiculoServices)
        {
            this.iVeiculoServices = iVeiculoServices;
        }

        [HttpGet]
        [Route("Listar/{data}")]
        [Authorize]
        //[GzipCompression]
        [CacheOutput(ClientTimeSpan = 10, ServerTimeSpan = 10)]
        public async Task<HttpResponseMessage> GetVeiculo(string data)
        {
            try
            {

                DateTime? _date = Convert.ToDateTime(data);

                var identity = User.Identity as ClaimsIdentity;

                var claims = from c in identity.Claims select c;

                var dto = Mapper.Map<List<Veiculo>, List<VeiculoModels>>(iVeiculoServices.GetVeiculo(Convert.ToInt32(SecurityDb.Decrypt(claims.ToList()[5].Value)), _date));

                return Request.CreateResponse(HttpStatusCode.OK, dto);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpGet]
        [Route("Listar/{data}/{IMEI}")]
        [Authorize]
        //[GzipCompression]
        [CacheOutput(ClientTimeSpan = 10, ServerTimeSpan = 10)]
        public async Task<HttpResponseMessage> GetVeiculo(string data, string IMEI)
        {
            try
            {
                DateTime? _date = Convert.ToDateTime(data);

                var identity = User.Identity as ClaimsIdentity;

                var claims = from c in identity.Claims select c;

                var dto = Mapper.Map<Veiculo, VeiculoModels>(iVeiculoServices.GetVeiculo(Convert.ToInt32(SecurityDb.Decrypt(claims.ToList()[5].Value)), _date, IMEI));

                return Request.CreateResponse(HttpStatusCode.OK, dto);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPut]
        [Route("Alterar/{data}/{IMEI}")]
        [Authorize]
        public async Task<HttpResponseMessage> Alterar(string data, string IMEI)
        {
            try
            {
                var dto = Mapper.Map<Veiculo, VeiculoDto>(iVeiculoServices.GetVeiculo(IMEI));

                if (dto != null)
                {
                    if (data == "S")
                    {
                        dto.comandoBloqueo = true;
                        dto.bloqueado = true;
                        dto.avisoBloqueio = false;

                        iVeiculoServices.Alterar(dto);
                     
                    }
                    else
                    {
                        if (data == "N")
                        {
                            dto.comandoBloqueo = true;
                            dto.bloqueado = false;
                            dto.avisoBloqueio = false;

                            iVeiculoServices.Alterar(dto);
                        }
                    }
                   
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Veículo não encontrado!");

                }

                return Request.CreateResponse(HttpStatusCode.OK, "Em alguns minutos, o seu veículo será " + (data == "S" ? "bloqueado" : "desbloqueado ") +"!");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
