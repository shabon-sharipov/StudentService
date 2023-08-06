namespace Application.Common.interfaces.Repositoties;

public interface IRepository<TEntity>
{
    Task<TEntity> FindAsync(string id, TEntity entity, CancellationToken cancellationToken = default);

    Task<string> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    string Delete(string entity);

    Task<string> Update(TEntity entity, string id, CancellationToken cancellationToken);
}