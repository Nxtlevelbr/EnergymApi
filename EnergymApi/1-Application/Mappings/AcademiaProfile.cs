using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Mappings
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