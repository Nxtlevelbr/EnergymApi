using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Mappings
{
    /// <summary>
    /// Configuração de mapeamento para a entidade RegistroExercicio.
    /// </summary>
    public class RegistroExercicioProfile : Profile
    {
        /// <summary>
        /// Inicializa uma nova instância de <see cref="RegistroExercicioProfile"/>.
        /// </summary>
        public RegistroExercicioProfile()
        {
            CreateMap<RegistroExercicioDto, RegistroExercicio>()
                .ForMember(dest => dest.Usuario, opt => opt.Ignore()) // Ignora a propriedade Usuario ao mapear
                .ReverseMap();
        }
    }
}