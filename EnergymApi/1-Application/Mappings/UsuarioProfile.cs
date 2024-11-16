using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._2_Domain.Models;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioDto>()
            .ReverseMap()
            .ForMember(dest => dest.Senha, opt => opt.Ignore())
            .ForMember(dest => dest.RegistrosExercicios, opt => opt.Ignore())
            .ForMember(dest => dest.Resgates, opt => opt.Ignore());
    }
}