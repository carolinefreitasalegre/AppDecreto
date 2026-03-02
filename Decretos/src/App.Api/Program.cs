using System.Security.Claims;
using System.Text;
using App.Application;
using App.Application.Interfaces;
using App.Application.Interfaces.Repository;
using App.Application.Middlewares;
using App.Application.Services;
using App.Application.Validations;
using App.Domain.Enums;
using App.Infrastructure;
using App.Infrastructure.Data;
using App.Infrastructure.Data.DbConnection;
using App.Infrastructure.Data.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dataSourceBuilder = new NpgsqlDataSourceBuilder(
    builder.Configuration.GetConnectionString("DefaultConnection")
);

dataSourceBuilder.MapEnum<Secretaria>("secretaria_enum");
dataSourceBuilder.MapEnum<UsuarioRole>("usuario_role_enum");
dataSourceBuilder.MapEnum<UsuarioStatus>("usuario_status_enum");

var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(dataSource)
);

builder.Services.AddScoped<DbConnectionFactory>();



builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IDecretoRepository, DecretoRepository>();
builder.Services.AddScoped<IDecretoRepository, DecretoRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IDecretoService, DecretoService>();

builder.Services.AddScoped<IHashSenhaService, HashSenhaService>();

builder.Services.AddScoped<IValidator<CriarUsuarioDto>, UsuarioValidator>();
builder.Services.AddScoped<IValidator<AtualizarUsuarioDto>, AtualizarUsuarioValidator>();
builder.Services.AddScoped<IValidator<CriarDecretoDto>, DecretoValidator>();
builder.Services.AddScoped<IValidator<AtualizarDecretoDto>, EdicaoDecretoValidator>();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRouting(opt => opt.LowercaseUrls = true); 



//autenticacao
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.Name
    };
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();

app.Run();



