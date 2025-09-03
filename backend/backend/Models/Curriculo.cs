using System.Globalization;

namespace backend.Models
{
    public class Curriculo
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public required string CargoDesejado { get; set; }
        public required string Escolaridade { get; set; }
        public string? Observacao { get; set; }
        public DateTime DataCriado { get; set; }
        public required byte[] Arquivo { get; set; }
        public required string NomeArquivo { get; set; }
    }

    public class CurriculoInput
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public required string CargoDesejado { get; set; }
        public required string Escolaridade { get; set; }
        public string? Observacao { get; set; }
        public required IFormFile Arquivo { get; set; }
    }
}
