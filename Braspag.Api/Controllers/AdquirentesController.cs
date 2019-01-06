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
    [RoutePrefix("api/adquirente")]
    public class AdquirentesController : ApiController
    {
        private readonly IAdiquirentesServices service;
        private readonly ILogServices log;

        public AdquirentesController(IAdiquirentesServices service, ILogServices log)
        {
            this.service = service;
            this.log = log;
        }

        [HttpGet]
        [Route("Lista")]
        [Authorize]
        public async Task<HttpResponseMessage> GetAdquirente()
        {
            try
            {
               var dados = AutoMapper.Mapper.Map<List<Adquirentes>, List<AdquirenteModels>>(service.GetLista().ToList());

               return Request.CreateResponse(HttpStatusCode.OK, dados);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [HttpPut]
        [Route("Alterar")]
        [Authorize]
        public async Task<HttpResponseMessage> Alterar([FromBody] AdquirenteModels value)
        {
            try
            {
                var dto = AutoMapper.Mapper.Map<AdquirenteModels, AdquirentesDto>(value);

                dto = AutoMapper.Mapper.Map<Adquirentes, AdquirentesDto>(service.GetByAdquirentes(dto));

                dto.elo = value.elo;
                dto.master = value.master;
                dto.visa = value.visa;

                var dados =  service.Alterar(dto);

                var dtoLog = new LogDto()
                {
                    json = JsonConvert.SerializeObject(value)
                };

                log.Cadastrar(dtoLog);

               return Request.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map<Adquirentes, AdquirenteModels>(dados));

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("Cadastrar")]
        [Authorize]
        public async Task<HttpResponseMessage> Cadastrar([FromBody] AdquirenteModels value)
        {
            try
            {
                var dto = AutoMapper.Mapper.Map<AdquirenteModels, AdquirentesDto>(value);

                var dados = service.Cadastrar(dto);

                var dtoLog = new LogDto()
                {
                    json = JsonConvert.SerializeObject(value)
                };

                log.Cadastrar(dtoLog);

                return Request.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map<Adquirentes, AdquirenteModels>(dados));

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



    }
}
