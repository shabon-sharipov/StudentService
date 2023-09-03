using Application.Repositories;
using Infrastructure.DataBase;
using Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repository;

public class Repository<TEntity> : IRepository<TEntity>
{
    private DatabaseContext _efContext;

    public Repository(IConfiguration configuration)
    {
        _efContext = new(configuration);
    }

    public async Task<TEntity> FindAsync(string id, string entityName, CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = _efContext.MongoDatabase().GetCollection<TEntity>(entityName);
            var filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
            var result = await entities.Find(filter).FirstOrDefaultAsync();

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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
            throw new Exception("Something wrong, in the add data. StudentService");
        }
    }

    public async Task<string> Delete(string id, string entityName)
    {
        try
        {
            var entities = _efContext.MongoDatabase().GetCollection<TEntity>(entityName);
            var filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));

            // Find the document to be deleted
            var document = await entities.FindOneAndDeleteAsync(filter);

            return "successful";

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<string> Update(TEntity entity, string id, CancellationToken cancellationToken)
    {
        try
        {
            var tableName = entity?.GetType().Name;
            var entities = _efContext.MongoDatabase().GetCollection<TEntity>(tableName);
            var filter = Builders<TEntity>.Filter.Eq("_id", new ObjectId(id));
            var result = await entities.Find(filter).FirstOrDefaultAsync();

            if (result == null)
                throw new NullReferenceException();

            await entities.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
            return "successful";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}