using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiManicure.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Preencha o seu nome.")]
        [StringLength(100, ErrorMessage = "O nome do cliente deve ter no máximo 100 caracteres.")]
        public string ClienteNome { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Preencha o seu telefone.")]
        [StringLength(20, ErrorMessage = "O telefone deve ter 20 caracteres.")]
        public string ClienteTelefone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Selecione a data.")]
        public DateTime DataAgendamento { get; set; }

        [ForeignKey("HorarioDisponivel")]
        public int HorarioDisponivelId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Preencha o serviço.")]
        [StringLength(20, ErrorMessage = "O servico deve ter 20 caracteres.")]
        public string Servico { get; set; } = string.Empty;
    }
}

