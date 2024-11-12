using AutoMapper;
using EnergyApi.DTOs;
using EnergyApi.Models;

namespace EnergyApi.Mappings
{
    public class AcademiaProfile : Profile
    {
        public AcademiaProfile()
        {
            // Mapeamento entre Academia e AcademiaDto
            CreateMap<Academia, AcademiaDto>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco))
                .ReverseMap();
            
            // Outros mapeamentos relacionados, se necessário
            CreateMap<Endereco, EnderecoDto>().ReverseMap();
        }
    }
}