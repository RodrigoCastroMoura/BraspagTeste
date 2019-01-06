using System;
using AutoMapper;
using RastreioFacil.Domain.Entities;
using RastreioFacil.Web.Models;
using RastreioFacil.Domain.DTO;


namespace RastreioFacil.Web.Mappers
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {

            Mapper.Initialize(x =>
            {
                x.CreateMap<Cliente, ClienteModels>();
                x.CreateMap<Veiculo, VeiculoModels>();
                x.CreateMap<DadosVeiculo, DadosVeiculoModels>();
                x.CreateMap<Cliente, ClienteDto>();

                x.CreateMap<Veiculo, VeiculoDto>()
                    .ForMember(opt => opt.DadosVeiculo, opt => opt.Ignore())
                    .ForMember(src => src.Rastreador, opt => opt.Ignore());

            });


            Mapper.AssertConfigurationIsValid();

       
        }
    }
}