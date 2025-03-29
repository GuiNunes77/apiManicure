using System.ComponentModel.DataAnnotations;

namespace apiManicure.Models
{
    public class LogTransacao
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataHora { get; set; } = DateTime.UtcNow;
        public string Tipo { get; set; } = "Erro de tipo";
        public string Descricao { get; set; } = string.Empty;
    }
}
