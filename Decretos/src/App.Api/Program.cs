using System.Text;
using App.Application;
using App.Application.Interfaces;
using App.Application.Interfaces.Repository;
using App.Application.Middlewares;
using App.Application.Services;
using App.Application.Validations;
using App.Domain.Enums;
using App.Infrastructure.Data.DbConnection;
using App.Infrastructure.Data.Repositories;
using App.Infrastructure.Messaging;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "Insira o Token"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

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

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IDecretoRepository, DecretoRepository>();
builder.Services.AddScoped<IDecretoRepository, DecretoRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IDecretoService, DecretoService>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddTransient<IHashSenhaService, HashSenhaService>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            
            ValidateAudience = true,
            ValidAudience = jwtAudience,

            ValidateLifetime = true,
            
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddTransient<IValidator<CriarUsuarioDto>, UsuarioValidator>();
builder.Services.AddTransient<IValidator<AtualizarUsuarioDto>, AtualizarUsuarioValidator>();
builder.Services.AddTransient<IValidator<CriarDecretoDto>, DecretoValidator>();
builder.Services.AddTransient<IValidator<AtualizarDecretoDto>, EdicaoDecretoValidator>();


builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"]);
            h.Password(builder.Configuration["RabbitMQ:Password"]);
        });
    });
});

var app = builder.Build();

app.UseMiddleware<CultureMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseAuthentication();

app.UseAuthentication();

app.UseAuthorization();
            
app.Run();



