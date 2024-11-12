using System;
using EnergyApi.Data;
using EnergyApi.Repositories;
using EnergyApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Configurar a string de conexão
string? connectionString = builder.Configuration.GetConnectionString("OracleConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'OracleConnection' não foi encontrada no appsettings.json.");
}

// Registrar ApplicationDbContext com timeout configurado
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseOracle(connectionString, oracleOptions =>
    {
        oracleOptions.CommandTimeout(60);
    });
});

// Registrar Controladores
builder.Services.AddControllers();

// Configurar Swagger para documentação e habilitar anotações
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EnergyApi",
        Version = "v1",
        Description = "API para gerenciamento de Energia Sustentável nas Academias",
        Contact = new OpenApiContact
        {
            Name = "Suporte EnergyApi",
            Email = "suporte@energyapi.com",
            Url = new Uri("https://energyapi.com/support")
        }
    });
    c.EnableAnnotations(); // Habilita o uso de anotações no Swagger
});

// Registrar repositórios e serviços para injeção de dependência
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IAcademiaRepository, AcademiaRepository>();
builder.Services.AddScoped<IAcademiaService, AcademiaService>();

builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();

builder.Services.AddScoped<IRegistroExercicioRepository, RegistroExercicioRepository>();
builder.Services.AddScoped<IRegistroExercicioService, RegistroExercicioService>();

builder.Services.AddScoped<IPremioRepository, PremioRepository>();
builder.Services.AddScoped<IPremioService, PremioService>();

builder.Services.AddScoped<IResgateRepository, ResgateRepository>();
builder.Services.AddScoped<IResgateService, ResgateService>();

var app = builder.Build();

// Configuração de middleware global para tratamento de exceções
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

// Configurar Swagger para UI e documentação
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnergyApi v1");
        c.RoutePrefix = string.Empty; // Acessível na raiz
    });
}

// Middleware de autorização
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// Alterar a URL da aplicação para porta 5070
app.Urls.Add("http://localhost:5070");

// Executar a aplicação
app.Run();
