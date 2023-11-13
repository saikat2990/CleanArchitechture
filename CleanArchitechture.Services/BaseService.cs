using AutoMapper;
using BookKeeping.Core.Interfaces.Services;
using CleanArchitechture.Core.DBEntities;
using CleanArchitechture.Core.Dtos;
using CleanArchitechture.Core.Interfaces.Repositories;

namespace BookKeeping.Services;

public class BaseService<TEntity, TDto, TKey> : IBaseService<TDto,TKey> where TEntity : BaseEntity<TKey> where TDto : BaseDto<TKey>
{
    private readonly IBaseRepository<TEntity, TKey> _repo;
    protected readonly IMapper _mapper;

    public BaseService(IBaseRepository<TEntity, TKey> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<List<TDto>> GetAll()
    {
        var list = await _repo.GetAll();
        return _mapper.Map<List<TDto>>(list);
    }

    public async Task<TDto> GetById(TKey Id)
    {
        var item = await _repo.GetById(Id);

        return _mapper.Map<TDto>(item);
    }

    public async Task<TDto> Create(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _repo.AddAsync(entity);
        return _mapper.Map<TDto>(entity);
    }

    public async Task Update(TDto dto)
    {
        var updatedEntity = _mapper.Map<TEntity>(dto);
        await _repo.UpdateAsync(updatedEntity);
    }

    public Task Delete(TKey Id)
    {
        throw new NotImplementedException();
    }

    public Task ParmanentDelete(TKey Id)
    {
        throw new NotImplementedException();
    }
}


//do not add properties/fields/methods to this class. Do that in the above class.

public class BaseService<TEntity,TDto> :BaseService<TEntity,TDto, int> , IBaseService<TDto> where TEntity : BaseEntity where TDto:BaseDto
{
    public BaseService(IBaseRepository<TEntity> repo, IMapper mapper) : base(repo,mapper)
    {
    }
}