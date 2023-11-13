using AutoMapper;
using CleanArchitechture.Core.DBEntities;
using CleanArchitechture.Core.Dtos;

namespace CleanArchitechture.Core.Interfaces.Common;

internal interface IMapFrom<T>
{
    IMapper Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
        profile.CreateMap(GetType(), typeof(T));
        var mapper = new MapperConfiguration(cfg => {

            cfg.CreateMap<UserCreateDto, AppUser>().ForMember(x => x.UserName, opt => opt.MapFrom(s => s.Name));

        }).CreateMapper();
        return mapper;
    }
}