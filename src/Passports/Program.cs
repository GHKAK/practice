using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Passports;
using Passports.Config;
using Passports.Data;
using Passports.Models;
using Passports.Repositories;
using Passports.Repositories.Interfaces;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddSingleton<IConnectionMultiplexer>(opt =>
// ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("redisPassports")));

var x = builder.Configuration.GetConnectionString("postgresPassports");
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<PassportContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("postgresPassports"));
    //opt.LogTo(Console.WriteLine);
});

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Logging.AddConsole();
builder.Services.AddScoped<PostgresRepository>();
builder.Services.AddScoped<IPassportRepository, LoggingPassportRepository>(provider =>
{
    var decoratedRepository = provider.GetService<PostgresRepository>();
    var logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigurationElastic.ConfigureElasticSink(builder.Configuration, environment))
        .CreateLogger();
    return new LoggingPassportRepository(decoratedRepository, logger);
});
builder.Services.AddScoped<LocalRepository>();
builder.Services.AddScoped<LocalRepositoryNew>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkContextDb>();

// builder.Services.AddScoped<IPassportRepository, RedisRepository>();
// builder.Services.AddScoped<IUnitOfWork, UnitOfWorkRedis>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
