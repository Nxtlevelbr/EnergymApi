using AutoMapper;
using EnergymApi._1_Application.DTO;
using EnergymApi._2_Domain.Models;

namespace EnergymApi._1_Application.Mappings;

public class PremioProfile : Profile
{
    public PremioProfile()
    {
        CreateMap<Premio, PremioDto>().ReverseMap();
    }
}