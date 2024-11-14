namespace EnergymApi._1_Application.DTO
{
    public class RegistroExercicioDto
    {
        public int Id { get; set; } // Identificador do registro de exercício

        public int UsuarioId { get; set; } // Identificador do usuário que realizou o exercício

        public string UsuarioNome { get; set; } // Nome do usuário (opcional, caso desejado para exibição)

        public int AcademiaId { get; set; } // Identificador da academia onde o exercício foi realizado

        public string AcademiaNome { get; set; } // Nome da academia (opcional, caso desejado para exibição)

        public double Km { get; set; } // Distância percorrida em quilômetros

        public DateTime DataHora { get; set; } // Data e hora do registro do exercício
    }
}