using CityService.Interface;
using CityService.Models;
using CityService.Process;
using CityService.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); 
var connstr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CityServiceDbContext>(option =>
{
    option.UseSqlServer(connstr);
});
builder.Services.AddScoped<ICity, CityRepository>();
builder.Services.AddScoped<CityProcess>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
