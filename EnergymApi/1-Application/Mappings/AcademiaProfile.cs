using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._2_Domain.Models;

public class AcademiaProfile : Profile
{
    public AcademiaProfile()
    {
        CreateMap<Academia, AcademiaDto>()
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco)) // Mapeia Endereco corretamente
            .ReverseMap()
            .ForMember(dest => dest.Endereco, opt => opt.Ignore()) // Ignora Endereco no ReverseMap, para evitar sobrescrita.
            .ForMember(dest => dest.EnderecoId, opt => opt.MapFrom(src => src.Endereco.Id)); // Mapeia EnderecoId.
    }
}