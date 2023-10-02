using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using backend.Models;


var builder = WebApplication.CreateBuilder(args);

// Add the NbaDbContext to the service collection with the connection string from appsettings.json
builder.Services.AddDbContext<NbaDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 34))));

// Add controllers as services to the container
builder.Services.AddControllers();

// Add API Explorer for endpoint discovery
builder.Services.AddEndpointsApiExplorer();

// Add Swagger documentation generation to the service collection
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS config
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:5173") 
           .AllowAnyHeader()                      
           .AllowAnyMethod();                     
});

app.UseHttpsRedirection();

// Enable authorization handling
app.UseAuthorization();

// Map controllers to their respective routes
app.MapControllers();

app.Run();
