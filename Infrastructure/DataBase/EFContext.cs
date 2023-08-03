using Infrastructure.Common;
using MongoDB.Driver;

namespace Infrastructure.DataBase;

public class EFContext
{
    private IMongoClient client;
    public EFContext() => client = new MongoClient(Constant.ConnacrionString);


    public IMongoDatabase MongoDatabase() => client.GetDatabase(Constant.DbName);
}
