using Application.Repositories;
using Infrastructure.Repository;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace StudentServiceApi.Extensions;

public static class ConfigureHostBuilderExtensions
{
    public static void AddSerilog(this ConfigureHostBuilder builder)
    {
        builder.UseSerilog((context, configuration) =>
        {
            configuration.Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticSearchConfiguration:Uri"]))
                    {
                        IndexFormat =
                            $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower()
                                .Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                        AutoRegisterTemplate = true,
                        NumberOfShards = 2,
                        NumberOfReplicas = 1
                    })
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .ReadFrom.Configuration(context.Configuration);
        });;
    }
}
