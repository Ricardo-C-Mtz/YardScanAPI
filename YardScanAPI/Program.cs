global using Microsoft.EntityFrameworkCore;
global using YardScanAPI.Data;
global using YardScanAPI.Helpers;
global using YardScanAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var AllowedOrigins = "http://localhost:8100";

// Add services to the container.

// Allow CORS Policy
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: "ionic",
    policy => 
    {
      policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

    });
});

// Register Db context
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
