namespace EnergymApi._1_Application.DTO
{
    /// <summary>
    /// DTO para representar um Endereço.
    /// </summary>
    public class EnderecoDto
    {
        /// <summary>
        /// Identificador único do endereço.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Código postal (CEP) do endereço.
        /// </summary>
        public string Cep { get; set; }

        /// <summary>
        /// Estado do endereço (Ex: SP, RJ).
        /// </summary>
        public string Estado { get; set; }

        /// <summary>
        /// Nome da cidade.
        /// </summary>
        public string Cidade { get; set; }

        /// <summary>
        /// Nome da rua.
        /// </summary>
        public string Rua { get; set; }

        /// <summary>
        /// Número do local.
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// Informação adicional sobre o endereço (complemento).
        /// </summary>
        public string? Complemento { get; set; }
    }
}