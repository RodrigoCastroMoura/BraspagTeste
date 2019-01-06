using AutoMapper.Mappers;
using Braspag.Api.Models;
using Braspag.Domain.DTO;
using Braspag.Domain.Entities;

namespace Braspag.Api.Mapper

{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {

            AutoMapper.Mapper.Initialize(x => {

                x.CreateMap<Adquirentes, AdquirenteModels>();

                x.CreateMap<AdquirenteModels,AdquirentesDto >()
                .ForMember(opt => opt._id, opt => opt.Ignore());

                x.CreateMap<Adquirentes, AdquirentesDto>();

                x.CreateMap<PagamentoModels, PagamentoDto>()
                .ForMember(opt => opt._id, opt => opt.Ignore());

                x.CreateMap<Pagamento, PagamentoModels>();

                x.CreateMap<Pagamento, PagamentoRetornoModels>();
               
            });

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}