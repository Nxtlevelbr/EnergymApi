using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Mappings
{
    public class EnderecoProfile : Profile
    {
        public EnderecoProfile()
        {
            CreateMap<EnderecoDto, Endereco>().ReverseMap();
        }
    }
}