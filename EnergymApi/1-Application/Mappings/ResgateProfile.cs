using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Mappings
{
    /// <summary>
    /// Configuração de mapeamento para a entidade Resgate.
    /// </summary>
    public class ResgateProfile : Profile
    {
        /// <summary>
        /// Inicializa uma nova instância de <see cref="ResgateProfile"/>.
        /// </summary>
        public ResgateProfile()
        {
            CreateMap<Resgate, ResgateDto>()
                .ForMember(dest => dest.UsuarioNome, opt => opt.MapFrom(src => src.Usuario.Username))
                .ForMember(dest => dest.PremioDescricao, opt => opt.MapFrom(src => src.Premio.Descricao))
                .ReverseMap()
                .ForMember(dest => dest.Usuario, opt => opt.Ignore()) // Ignora a propriedade Usuario no mapeamento reverso
                .ForMember(dest => dest.Premio, opt => opt.Ignore());  // Ignora a propriedade Premio no mapeamento reverso
        }
    }
}