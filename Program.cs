using APIBillExchange.Context;
using APIBillExchange.Data_Access;
using APIBillExchange.Interfaces;
using APIBillExchange.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Data access
builder.Services.AddDbContext<MoneyExchangeContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("conString")));

//Dependency inyection
builder.Services.AddScoped<IVueltoService, VueltoService>();
builder.Services.AddScoped<DivisaDA>();
builder.Services.AddScoped<OperacionDA>();
builder.Services.AddScoped<TipoDivisaDA>();
builder.Services.AddScoped<TransaccionCambioDA>();

var app = builder.Build();

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
