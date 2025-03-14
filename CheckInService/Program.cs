using CheckInService.Process;
using CheckInService.Repository;
using FMSLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CheckInDbContext>(c =>
{
    c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<ICheckIn, CheckInRepository>();
builder.Services.AddScoped<CheckInProcess>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
app.UseCors(c =>
{
    c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
