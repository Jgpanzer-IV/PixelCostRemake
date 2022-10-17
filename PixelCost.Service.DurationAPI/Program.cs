using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PixelCost.Service.DurationAPI.Database;
using PixelCost.Service.DurationAPI.Models.DTOs;
using PixelCost.Service.DurationAPI.Models.Entities;
using PixelCost.Service.DurationAPI.Services.Implementations;
using PixelCost.Service.DurationAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddXmlDataContractSerializerFormatters()
    .AddXmlSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(configAction => {
    configAction.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "Duration API",
        Description = "This api responsible to serve the 'Duration' entity and its releated infomation that using orther entity.",
        Contact = new OpenApiContact
        {
            Name = "Karnchai Sakkarnjana",
            Email = "Sakkarnjana@outlook.com",
            Url = new Uri(builder.Configuration["AdminContect"])
        },
        License = new OpenApiLicense
        {
            Name = "Pixel License",
            Url = new Uri(builder.Configuration["ClientUrl:PixelLicense"].ToString())
        },
        TermsOfService = new Uri(builder.Configuration["ClientUrl:TermOfService"].ToString())
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(config => config.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddScoped<IDurationRepository, DurationRepository>();

IMapper MapperService = new MapperConfiguration(config => {

    config.CreateMap<Duration, DurationDTO>().ReverseMap();
    config.CreateMap<SubDuration, SubDurationDTO>().ReverseMap();
    config.CreateMap<Category, CategoryDTO>().ReverseMap();
    config.CreateMap<PrimaryExpense, PrimaryExpenseDTO>().ReverseMap();
    config.CreateMap<Revenue, RevenueDTO>().ReverseMap();

}).CreateMapper();
builder.Services.AddSingleton(MapperService);
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
