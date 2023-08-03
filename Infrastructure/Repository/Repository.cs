using Application.Common.interfaces.Repositoties;
using Infrastructure.DataBase;

namespace Infrastructure.Repository;

public class Repository<TEntity> : IRepository<TEntity>
{
    private EFContext _efContext;

    public Repository()
    {
        _efContext = new();
    }

    public IQueryable<TEntity> GetAll(int pageSize, int pageNumber, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<TEntity>> GetAllAsync(int pageSize, int pageNumber, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TEntity> Set()
    {
        throw new NotImplementedException();
    }

    public TEntity Find(ulong id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> FindAsync(ulong id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Add(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<string> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var tableName = entity?.GetType().Name + "s";
            var entities = _efContext.MongoDatabase().GetCollection<TEntity>(tableName);
            await entities.InsertOneAsync(entity, cancellationToken: cancellationToken);
            return "successfull";
        }
        catch (Exception e)
        {
           throw new Exception();
        }
    }

    public void Add(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Update(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public int SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}