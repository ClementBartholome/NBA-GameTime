using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using backend.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NbaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NbaContext") ?? throw new InvalidOperationException("Connection string 'NbaContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<NbaDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 34)));

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
