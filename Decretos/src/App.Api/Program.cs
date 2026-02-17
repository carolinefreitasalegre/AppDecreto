using App.Application.Interfaces;
using App.Application.Interfaces.Repository;
using App.Application.Services;
using App.Domain.Enums;
using App.Infrastructure;
using App.Infrastructure.Data;
using App.Infrastructure.Data.Repositories;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DbConnectionFactory>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IDecretoRepository, DecretoRepository>();
builder.Services.AddScoped<IDecretoRepository, DecretoRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IDecretoService, DecretoService>();


builder.Services.AddScoped<IHashSenhaService, HashSenhaService>();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();



