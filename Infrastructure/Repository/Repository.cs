using Application.Common.interfaces.Repositoties;
using Infrastructure.DataBase;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Infrastructure.Repository;

public class Repository<TEntity> : IRepository<TEntity>
{
    private EFContext _efContext;

    public Repository()
    {
        _efContext = new();
    }

    public async Task<TEntity> FindAsync(string id, TEntity entity, CancellationToken cancellationToken = default)
    {
        var tableName = entity?.GetType().Name;
        var entities = _efContext.MongoDatabase().GetCollection<TEntity>(tableName);

        var filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
        var result = await entities.Find(filter).FirstOrDefaultAsync();

        return result;
    }

    public async Task<string> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var tableName = entity?.GetType().Name;
            var entities = _efContext.MongoDatabase().GetCollection<TEntity>(tableName);
            await entities.InsertOneAsync(entity, cancellationToken: cancellationToken);
            return "successful";
        }
        catch (Exception e)
        {
            throw new Exception();
        }
    }

    public string Delete(string entity)
    {
        return "successful";
    }

    public async Task<string> Update(TEntity entity, string id, CancellationToken cancellationToken)
    {
        var tableName = entity?.GetType().Name;
        var entities = _efContext.MongoDatabase().GetCollection<TEntity>(tableName);
        var filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
        var result = await entities.Find(filter).FirstOrDefaultAsync();

        if (result == null)
            throw new NullReferenceException();
        try
        {
            await entities.ReplaceOneAsync(filter, entity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return "successful";
    }
}