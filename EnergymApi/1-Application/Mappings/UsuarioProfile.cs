using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Mappings
{
    /// <summary>
    /// Configuração de mapeamento para a entidade Usuario.
    /// </summary>
    public class UsuarioProfile : Profile
    {
        /// <summary>
        /// Inicializa uma nova instância de <see cref="UsuarioProfile"/>.
        /// </summary>
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDto>()
                .ReverseMap()
                .ForMember(dest => dest.RegistrosExercicios, opt => opt.Ignore()) // Ignora a coleção de RegistrosExercicios no mapeamento reverso
                .ForMember(dest => dest.Resgates, opt => opt.Ignore()); // Ignora a coleção de Resgates no mapeamento reverso
        }
    }
}