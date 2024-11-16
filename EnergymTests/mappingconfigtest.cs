using AutoMapper;
using EnergymApi._1_Application.Mappings;
using Xunit;

namespace EnergymTests;

public class mappingconfigtest
{
    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UsuarioProfile());
            cfg.AddProfile(new AcademiaProfile());
            cfg.AddProfile(new PremioProfile());
            cfg.AddProfile(new EnderecoProfile());
            cfg.AddProfile(new RegistroExercicioProfile());
            cfg.AddProfile(new ResgateProfile());
        });

        config.AssertConfigurationIsValid(); // Lança exceção se houver mapeamento inválido.
    }

}