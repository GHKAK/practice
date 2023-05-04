using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<TodoContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("postgresUsers")));
builder.Services.AddTransient<IUsersRepository,UsersRepository>();
builder.Services.AddControllers();
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
