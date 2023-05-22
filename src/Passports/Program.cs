using Microsoft.EntityFrameworkCore;
using Passports.Data;
using Passports.Models;
using Passports.Repositories;
using Passports.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<PassportContext>(opt => {
    opt.UseNpgsql(builder.Configuration.GetConnectionString("postgresPassports"));
    opt.LogTo(Console.WriteLine);
});
builder.Services.AddScoped<PostgresRepository>();
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
