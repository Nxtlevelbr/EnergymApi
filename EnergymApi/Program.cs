using System.Reflection;
using AutoMapper;
using EnergyApi.Services;
using EnergymApi._1_Application.Interfaces;
using EnergymApi._1_Application.Mappings;
using EnergymApi._1_Application.Services;
using EnergymApi._2_Domain.Interfaces;
using EnergymApi._3_Infrastructure.Data;
using EnergymApi._3_Infrastructure.Data.Repositories;
using EnergymApi._3_Infrastructure.ML.Services;
using Microsoft.EntityFrameworkCore;
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

// Registrar perfis do AutoMapper explicitamente
builder.Services.AddSingleton(provider => 
    new MapperConfiguration(cfg => 
    {
        cfg.AddProfile(new UsuarioProfile());
        cfg.AddProfile(new AcademiaProfile());
        cfg.AddProfile(new PremioProfile());
        cfg.AddProfile(new EnderecoProfile());
        cfg.AddProfile(new RegistroExercicioProfile());
        cfg.AddProfile(new ResgateProfile());
    }).CreateMapper()
);

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
    c.EnableAnnotations();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
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

// Registrar o serviço de recomendação de prêmios com ML.NET
builder.Services.AddSingleton<PremioRecomendationService>();

var app = builder.Build();

// Middleware global de tratamento de exceções
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { error = "Um erro inesperado ocorreu. Tente novamente mais tarde." });
        });
    });
}
else
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
