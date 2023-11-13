
using CleanArchitechture.Core.Dtos;

namespace CleanArchitechture.Core.Interfaces.Services;

public interface IBaseService<TDto, TKey> where TDto : BaseDto<TKey>
{
    Task<List<TDto>> GetAll();
    Task<TDto> GetById(TKey Id);
    Task<TDto> Create(TDto dto);
    Task Update(TDto dto);
    Task Delete(TKey Id);
    Task ParmanentDelete(TKey Id);
}


//do not add properties/fields/methods to this class. Do that in the above class.
public interface IBaseService<TDto>:IBaseService<TDto,int> where TDto:BaseDto{}