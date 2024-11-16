using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Mappings
{
    public class RegistroExercicioProfile : Profile
    {
        public RegistroExercicioProfile()
        {
            CreateMap<RegistroExercicioDto, RegistroExercicio>().ReverseMap();
        }
    }
}