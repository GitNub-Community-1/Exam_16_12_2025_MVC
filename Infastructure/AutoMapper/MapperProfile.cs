using AutoMapper;
using Domain.Models;
using Domain.ViewModels;

namespace Infastructure.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<PostAddViewModel, Post>().ReverseMap();
        CreateMap<PostUpdateViewModel, Post>().ReverseMap();
        CreateMap<TagAddViewModel, Tag>().ReverseMap();
        CreateMap<TagUpdateViewModel, Tag>().ReverseMap();
    }
}