using CleanArchitechture.Core.DBEntities;
using CleanArchitechture.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitechture.Repositories.Repositories;

public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>

{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await All().ToListAsync();
    }

    public async Task<TEntity> GetById(TKey id)
    {
        return (await All().Where(x => x.Id.Equals(id)).FirstOrDefaultAsync())!;
    }

    public void AddToContext(TEntity entity)
    {
        _context.Add(entity);
    }

    public bool Add(TEntity entity)
    {
        _context.Add(entity);
        return SaveChanges();
    }

    public virtual async Task<bool> AddAsync(TEntity entity)
    {
        _context.Add(entity);
        return await SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public void AddListToContext(IEnumerable<TEntity> entities)
    {
        _context.AddRange(entities);
    }

    public bool AddList(IEnumerable<TEntity> entities)
    {
        AddListToContext(entities);
        return SaveChanges();
    }

    protected IQueryable<TEntity> All()
    {
        return _dbSet.Where(x => !x.IsDeleted);
    }

    protected IQueryable<TEntity> AsQuery()
    {
        return _dbSet.AsQueryable();
    }
    public void DeleteFromContext(TEntity entity)
    {
        entity.IsDeleted = true;
    }

    public void ParmanentDeleteFromContext(TEntity entity)
    {
        _context.Remove(entity);
    }

    public void Delete(TEntity entity)
    {
        DeleteFromContext(entity);
        SaveChanges();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        DeleteFromContext(entity);
        await SaveChangesAsync();
    }

    public async Task ParmentDeleteAsync(TEntity entity)
    {
        ParmanentDeleteFromContext(entity);
        await SaveChangesAsync();
    }

    public void DeleteListFromContext(IEnumerable<TEntity> entities)
    {
        if (entities?.Any() == true)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }
        }
    }

    public void ParmanentDeleteListFromContext(IEnumerable<TEntity> entities)
    {
        if (entities?.Any() == true)
        {
            _context.RemoveRange(entities);
        }
    }

    public void DeleteList(IEnumerable<TEntity> entities)
    {
        DeleteListFromContext(entities);
        _context.SaveChanges();
    }

    public void ParmanentDeleteList(IEnumerable<TEntity> entities)
    {
        ParmanentDeleteListFromContext(entities);
        _context.SaveChanges();
    }

    public async Task DeleteListAsync(IEnumerable<TEntity> entities)
    {
        DeleteListFromContext(entities);
        await _context.SaveChangesAsync();
    }

    public async Task ParmanentDeleteListAsync(IEnumerable<TEntity> entities)
    {
        ParmanentDeleteListFromContext(entities);
        await _context.SaveChangesAsync();
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}

public class BaseRepository<T> : BaseRepository<T, int> where T : BaseEntity
{
    public BaseRepository(AppDbContext context) : base(context)
    {
        //var g = Guid.NewGuid().ToString()
    }
}
//do not add properties/fields/methods to this class. Do that in the above class.