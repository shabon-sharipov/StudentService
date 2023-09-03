namespace Application.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity> FindAsync(string id, string entityName, CancellationToken cancellationToken = default);

    Task<string> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<string> Delete(string id, string entityName);

    Task<string> Update(TEntity entity, string id, CancellationToken cancellationToken);
}