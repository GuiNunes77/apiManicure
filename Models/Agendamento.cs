using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiManicure.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ClienteNome { get; set; }

        [Required]
        [StringLength(20)]
        public string ClienteTelefone { get; set; }

        [ForeignKey("HorarioDisponivel")]
        public int HorarioDisponivelId { get; set; }

        public HorarioDisponivel HorarioDisponivel { get; set; }
    }
}

