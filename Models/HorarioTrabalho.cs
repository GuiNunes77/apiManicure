using System.ComponentModel.DataAnnotations;

namespace apiManicure.Models
{
    public class HorarioTrabalho
    {
        [Key]
        public int Id { get; set; }
        public int DiaSemana { get; set; } // 0 = Domingo, 1 = Segunda...
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public int DuracaoAtendimento { get; set; } // minutos
    }
}
