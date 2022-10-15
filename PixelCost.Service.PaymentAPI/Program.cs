using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PixelCost.Service.PaymentAPI.Database;
using PixelCost.Service.PaymentAPI.Models.DTOs;
using PixelCost.Service.PaymentAPI.Models.Entities;
using PixelCost.Service.PaymentAPI.Services.Implementations;
using PixelCost.Service.PaymentAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));


builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();


IMapper MapperService = new MapperConfiguration(config => {
    config.CreateMap<Expense, ExpenseDTO>().ReverseMap();
    config.CreateMap<PrimaryExpense, PrimaryExpenseDTO>().ReverseMap();
    config.CreateMap<Revenue, RevenueDTO>().ReverseMap();
    config.CreateMap<PaymentMethod, PaymentMethodDTO>().ReverseMap();
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
