using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Passports.Data;
using Passports.Models;
using Passports.Repositories;
using Passports.Repositories.Interfaces;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets("ea88d7a2-ed79-45f4-b3a9-0e4670cbe894");

// Add services to the container.
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<PassportContext>(opt => {
    opt.UseNpgsql(builder.Configuration.GetConnectionString("postgresPassports"));
    //opt.LogTo(Console.WriteLine);
});
//builder.Logging.AddConsole();
builder.Services.AddScoped<PostgresRepository>();
builder.Services.AddScoped<IPassportRepository, LoggingPassportRepository>(provider => {
    var decoratedRepository = provider.GetService<PostgresRepository>();
    var logger =  new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new[]{new Uri("http://localhost:5601/")}))
        .CreateLogger();
    return new LoggingPassportRepository(decoratedRepository, logger);
});
builder.Services.AddScoped<LocalRepository>();
builder.Services.AddScoped<LocalRepositoryNew>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
