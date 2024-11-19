using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Mappings
{
    /// <summary>
    /// Configuração de mapeamento para a entidade Academia.
    /// </summary>
    public class AcademiaProfile : Profile
    {
        /// <summary>
        /// Inicializa uma nova instância de <see cref="AcademiaProfile"/>.
        /// </summary>
        public AcademiaProfile()
        {
            CreateMap<Academia, AcademiaDto>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco)) // Mapeia Endereco corretamente.
                .ReverseMap()
                .ForMember(dest => dest.Endereco, opt => opt.Ignore()) // Ignora Endereco no ReverseMap para evitar sobrescrita.
                .ForMember(dest => dest.EnderecoId, opt => opt.MapFrom(src => src.Endereco.Id)); // Mapeia EnderecoId corretamente.
        }
    }
}