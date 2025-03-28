using Microsoft.EntityFrameworkCore;
using apiManicure.Models;

namespace apiManicure.Infra
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<HorarioTrabalho> HorariosTrabalho { get; set; }
        public DbSet<HorarioDisponivel> HorariosDisponiveis { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}