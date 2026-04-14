using AutoMapper;
using Domain.Layer.DTOs;
using Domain.Layer.Models;

public class ToyMappingProfile : Profile
{
    public ToyMappingProfile()
    {
        CreateMap<Toy, ToyDto>();
    }
}