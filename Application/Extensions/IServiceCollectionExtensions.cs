using Application.Consumer;
using Application.Mappers;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IStudentPublisher, StudentPublisher>();
        services.AddScoped<IStudentService, StudentService>();

        services.AddAutoMapper(typeof(AutoMapperConfiguration).Assembly);

        return services;
    }
}
