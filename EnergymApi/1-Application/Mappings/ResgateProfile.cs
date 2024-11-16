using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._2_Domain.Models;

public class ResgateProfile : Profile
{
    public ResgateProfile()
    {
        CreateMap<Resgate, ResgateDto>()
            .ForMember(dest => dest.UsuarioNome, opt => opt.MapFrom(src => src.Usuario.Username))
            .ForMember(dest => dest.PremioDescricao, opt => opt.MapFrom(src => src.Premio.Descricao))
            .ReverseMap()
            .ForMember(dest => dest.Usuario, opt => opt.Ignore()) // Ignorar a entidade completa no mapeamento reverso
            .ForMember(dest => dest.Premio, opt => opt.Ignore());  // Ignorar a entidade completa no mapeamento reverso
    }
}