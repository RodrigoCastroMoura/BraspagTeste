using Braspag.Api.Models;
using Braspag.Domain.DTO;
using Braspag.Domain.Entities;
using Braspag.Domain.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Braspag.Api.Controllers
{

    [RoutePrefix("api/Pagamento")]
    public class PagamentoController : ApiController
    {
        private readonly IPagamentoService service;

        private readonly ILogServices log;

        public PagamentoController(IPagamentoService service, ILogServices log) {

            this.service = service;
            this.log = log;
        }


        [HttpPost]
        [Route("Cadastrar")]
        [Authorize]
        public async Task<HttpResponseMessage> Cadastrar ([FromBody] PagamentoModels value)
        {
            try
            {
                var dto = AutoMapper.Mapper.Map<PagamentoModels,PagamentoDto>(value);

                var retorno = service.CadastrarPagamento(dto);

                var dtoLog = new LogDto()
                {
                    json = JsonConvert.SerializeObject(value)
                };

                log.Cadastrar(dtoLog);

                return Request.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map <Pagamento , PagamentoRetornoModels>(retorno));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("CadastrarLista")]
        [Authorize]
        public async Task<HttpResponseMessage> CadastrarLista([FromBody] List<PagamentoModels> value)
        {
            try
            {
                var ListaModels = new List<PagamentoRetornoModels>();

                var dtoLog = new LogDto()
                {
                    json = JsonConvert.SerializeObject(value)
                };

                for (int i = 0; i < value.Count; i++)
                {
                    var dto = AutoMapper.Mapper.Map<PagamentoModels, PagamentoDto>(value[i]);

                    var retorno = service.CadastrarPagamento(dto);

                    ListaModels.Add(AutoMapper.Mapper.Map<Pagamento, PagamentoRetornoModels>(retorno));
                }

                log.Cadastrar(dtoLog);

                return Request.CreateResponse(HttpStatusCode.OK, ListaModels);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
