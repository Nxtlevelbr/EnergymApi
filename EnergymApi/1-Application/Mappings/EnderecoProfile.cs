using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Mappings
{
    /// <summary>
    /// Configuração de mapeamento para a entidade Endereco.
    /// </summary>
    public class EnderecoProfile : Profile
    {
        /// <summary>
        /// Inicializa uma nova instância de <see cref="EnderecoProfile"/>.
        /// </summary>
        public EnderecoProfile()
        {
            CreateMap<EnderecoDto, Endereco>()
                .ReverseMap(); 
        }
    }
}