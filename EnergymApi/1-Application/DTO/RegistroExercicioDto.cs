namespace EnergymApi._1_Application.DTO
{
    /// <summary>
    /// DTO para representar um registro de exercício.
    /// </summary>
    public class RegistroExercicioDto
    {
        /// <summary>
        /// Identificador único do registro de exercício.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador único do usuário associado ao registro.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Quilômetros percorridos durante o exercício.
        /// </summary>
        public double Km { get; set; }

        /// <summary>
        /// Data e hora em que o exercício foi registrado.
        /// </summary>
        public DateTime DataHora { get; set; }
    }
}