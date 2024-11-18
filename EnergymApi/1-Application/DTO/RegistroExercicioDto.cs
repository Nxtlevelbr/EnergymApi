namespace EnergymApi._1_Application.DTO
{
    public class RegistroExercicioDto
    {
        public int Id { get; set; } // ID do registro

        public int UsuarioId { get; set; } // ID do usuário

        public double Km { get; set; } // Quilômetros percorridos

        public DateTime DataHora { get; set; } // Data e hora do registro
    }
}