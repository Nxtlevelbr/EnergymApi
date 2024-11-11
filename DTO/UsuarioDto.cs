namespace EnergyApi.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; } // Identificador do usuário

        public string Username { get; set; } // Nome de usuário

        public string Email { get; set; } // Endereço de email

        public string CPF { get; set; } // CPF do usuário

        public DateTime DataNascimento { get; set; } // Data de nascimento

        public string Sexo { get; set; } // Sexo do usuário

        public int Pontos { get; set; } // Pontos acumulados pelo usuário
    }
}