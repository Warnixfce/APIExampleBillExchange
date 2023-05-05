using APIBillExchange.Context;
using APIBillExchange.Data_Access;
using APIBillExchange.Interfaces;
using APIBillExchange.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configurar el acceso a los datos.
builder.Services.AddDbContext<MoneyExchangeContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("conString")));

//Configurar las interfaces para que el controller las pueda usar.
builder.Services.AddScoped<IVueltoService, VueltoService>();
builder.Services.AddScoped<DivisaDA>();
builder.Services.AddScoped<OperacionDA>();
builder.Services.AddScoped<TipoDivisaDA>();
builder.Services.AddScoped<TransaccionCambioDA>();

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
