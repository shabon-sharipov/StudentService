using Application.Extensions;
using Infrastructure.Extensions;
using Serilog;
using StudentServiceApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddCors();
builder.Host.AddSerilog();

var app = builder.Build();
var configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .AddJsonFile($"appsettings.{app.Environment.EnvironmentName}.json")
  .AddEnvironmentVariables()
  .Build();
Console.WriteLine(builder.Configuration["RabbitMQ:Url"]);
Console.WriteLine(configuration["RabbitMQ:Url"]);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(o =>
{
    o.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin();
});
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();