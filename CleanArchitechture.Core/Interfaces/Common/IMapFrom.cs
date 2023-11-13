using AutoMapper;

namespace CleanArchitechture.Core.Interfaces.Common;

internal interface IMapFrom<T>
{
    void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
        profile.CreateMap(GetType(), typeof(T));
    }
}