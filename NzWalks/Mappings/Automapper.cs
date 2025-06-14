using System.Runtime;
using AutoMapper;
using NzWalks.Models.DomainModel;
using NzWalks.Models.DTOs;

namespace NzWalks.Mappings
{
    public class Automapper : Profile
    {

        public Automapper()
        {
            CreateMap<Region, regionDto>().ReverseMap();
            CreateMap<AddWalkDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty,DifficultyDto>().ReverseMap();
            CreateMap<updatewalkDto, Walk>().ReverseMap();

        }
    }
}
