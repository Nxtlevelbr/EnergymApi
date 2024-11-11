using EnergyApi.Data;
using EnergyApi.Repositories;
using EnergyApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Configurar a string de conexão
string? connectionString = builder.Configuration.GetConnectionString("OracleConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'OracleConnection' não foi encontrada no appsettings.json.");
}

// Registra o ApplicationDbContext com timeout configurado
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

// Middleware global para tratamento de exceções
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            await context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                Details = contextFeature?.Error.Message ?? "Erro desconhecido"
            }.ToString() ?? "");
        }
    });
});

// Configurar Swagger para UI e documentação
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnergyApi v1");
    c.RoutePrefix = string.Empty; // Swagger estará disponível na raiz
});

// Middleware de autorização
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// Executar a aplicação
app.Run();
