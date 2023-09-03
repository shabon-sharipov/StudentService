using Application.Repositories;
using Infrastructure.Options;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddOptions<MongoDbOptions>("MongoDb");
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
