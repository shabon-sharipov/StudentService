using Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.DataBase;

public class DatabaseContext
{
    private readonly MongoDbOptions _options;
    private readonly IMongoClient _client;
    public DatabaseContext(IConfiguration configuration)
    {
        _options = configuration.GetSection("MongoDb")?.Get<MongoDbOptions>() ?? throw new ArgumentNullException("MongoDb");
        _client = new MongoClient(_options.ConectionString);
    }

    public IMongoDatabase MongoDatabase() => _client.GetDatabase(_options.DatabaseName);
}
