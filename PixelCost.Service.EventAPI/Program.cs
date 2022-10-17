using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PixelCost.Service.EventAPI.Database;
using PixelCost.Service.EventAPI.Models.DTOs;
using PixelCost.Service.EventAPI.Models.Entities;
using PixelCost.Service.EventAPI.Services.Interfaces;
using PixelCost.Service.EventAPI.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(config => config.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddScoped<ISubDurationRepository, SubDurationRepository>();

IMapper MapperConfiguration = new MapperConfiguration(configExpress =>
{
    configExpress.CreateMap<SubDuration, SubDurationDTO>().ReverseMap();
    configExpress.CreateMap<Revenue, RevenueDTO>().ReverseMap();
    configExpress.CreateMap<Category, CategoryDTO>().ReverseMap();
}).CreateMapper();
builder.Services.AddSingleton(MapperConfiguration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());








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
