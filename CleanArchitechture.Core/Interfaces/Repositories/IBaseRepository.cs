using CleanArchitechture.Core.DBEntities;

namespace CleanArchitechture.Core.Interfaces.Repositories;

public interface IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    Task<List<TEntity>> GetAll();
    public Task<TEntity> GetById(TKey id);
    void AddToContext(TEntity entity);
    bool Add(TEntity entity);
    void AddListToContext(IEnumerable<TEntity> entities);
    bool AddList(IEnumerable<TEntity> entities);
    Task<bool> UpdateAsync(TEntity entity);
    void DeleteFromContext(TEntity entity);
    void Delete(TEntity entity);
    void DeleteListFromContext(IEnumerable<TEntity> entities);
    void DeleteList(IEnumerable<TEntity> entities);
    Task DeleteListAsync(IEnumerable<TEntity> entities);
    bool SaveChanges();
    Task<bool> SaveChangesAsync();

    Task<bool> AddAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task ParmentDeleteAsync(TEntity entity);
    void ParmanentDeleteFromContext(TEntity entity);
    void ParmanentDeleteList(IEnumerable<TEntity> entities);
    Task ParmanentDeleteListAsync(IEnumerable<TEntity> entities);
}

public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, int> where TEntity : BaseEntity { }
