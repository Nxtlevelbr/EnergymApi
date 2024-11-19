using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Mappings
{
    /// <summary>
    /// Configuração de mapeamento para a entidade Premio.
    /// </summary>
    public class PremioProfile : Profile
    {
        /// <summary>
        /// Inicializa uma nova instância de <see cref="PremioProfile"/>.
        /// </summary>
        public PremioProfile()
        {
            CreateMap<Premio, PremioDto>().ReverseMap();
        }
    }
}