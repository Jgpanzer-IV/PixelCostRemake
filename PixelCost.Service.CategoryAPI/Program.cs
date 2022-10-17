using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PixelCost.Service.CategoryAPI.Database;
using PixelCost.Service.CategoryAPI.Models.DTOs;
using PixelCost.Service.CategoryAPI.Models.Entities;
using PixelCost.Service.CategoryAPI.Services.Implementations;
using PixelCost.Service.CategoryAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddXmlDataContractSerializerFormatters().AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(config => config.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

IMapper MapperConfiguration = new MapperConfiguration(config => {
    config.CreateMap<Category, CategoryDTO>().ReverseMap();
    config.CreateMap<Expense, ExpenseDTO>().ReverseMap();
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
