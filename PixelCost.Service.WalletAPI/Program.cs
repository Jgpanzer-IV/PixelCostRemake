using AutoMapper;
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
builder.Services.AddSwaggerGen();


builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("SqlServer"));

builder.Services.AddScoped<IWalletRepository, WalletRepository>();

IMapper MapperConfiguration = new MapperConfiguration(express =>
{
    express.CreateMap<Wallet, WalletDTO>();
    express.CreateMap<PaymentMethod, PaymentMethodDTO>();

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
