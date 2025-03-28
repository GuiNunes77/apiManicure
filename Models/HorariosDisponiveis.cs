using System.ComponentModel.DataAnnotations;

namespace apiManicure.Models
{
    public class HorarioDisponivel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public bool Disponivel { get; set; } = true;
    }
}
