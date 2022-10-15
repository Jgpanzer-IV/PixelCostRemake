using AutoMapper;
using Microsoft.OpenApi.Models;
using PixelCost.Service.WalletAPI.Database;
using PixelCost.Service.WalletAPI.Model.DTOs;
using PixelCost.Service.WalletAPI.Model.Entities;
using PixelCost.Service.WalletAPI.Services.Implementations;
using PixelCost.Service.WalletAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option => {
    option.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "Wallet Api",
        Description = "This api responsible to serve the 'wallet' and its releated infomation that using 'PaymentMethod' entity.",
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


builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("SqlServer"));

builder.Services.AddScoped<IWalletRepository, WalletRepository>();

IMapper MapperConfiguration = new MapperConfiguration(express =>
{
    express.CreateMap<Wallet, WalletDTO>().ReverseMap();
    express.CreateMap<PaymentMethod, PaymentMethodDTO>().ReverseMap();

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
