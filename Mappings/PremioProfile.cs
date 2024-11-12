using AutoMapper;
using EnergyApi.DTOs;
using EnergyApi.Models;

public class PremioProfile : Profile
{
    public PremioProfile()
    {
        CreateMap<Premio, PremioDto>().ReverseMap();
    }
}