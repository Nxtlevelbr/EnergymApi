namespace EnergymApi._1_Application.DTO
{
    /// <summary>
    /// DTO para representar um usuário.
    /// </summary>
    public class UsuarioDto
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome de usuário.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Endereço de email do usuário.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// CPF do usuário.
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Data de nascimento do usuário.
        /// </summary>
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// Sexo do usuário.
        /// </summary>
        public string Sexo { get; set; }

        /// <summary>
        /// Pontos acumulados pelo usuário.
        /// </summary>
        public int Pontos { get; set; }
    }
}