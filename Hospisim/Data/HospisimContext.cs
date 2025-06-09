using Microsoft.EntityFrameworkCore;
using Hospisim.Models;

namespace Hospisim.Data
{
    public class HospisimContext : DbContext
    {
        public HospisimContext(DbContextOptions<HospisimContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }
        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Prescricao> Prescricoes { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Internacao> Internacoes { get; set; }
        public DbSet<AltaHospitalar> AltasHospitalares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // INTERNACAO -> PACIENTE (Muitos para Um) — SEM DELETE EM CASCATA
            modelBuilder.Entity<Internacao>()
                .HasOne(i => i.Paciente)
                .WithMany(p => p.Internacoes)
                .HasForeignKey(i => i.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // INTERNACAO -> ATENDIMENTO (Um para Um) — SEM DELETE EM CASCATA
            modelBuilder.Entity<Internacao>()
                .HasOne(i => i.Atendimento)
                .WithOne(a => a.Internacao)
                .HasForeignKey<Internacao>(i => i.AtendimentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // INTERNACAO -> ALTA (Um para Um) — SEM DELETE EM CASCATA
            modelBuilder.Entity<Internacao>()
                .HasOne(i => i.AltaHospitalar)
                .WithOne(a => a.Internacao)
                .HasForeignKey<AltaHospitalar>(a => a.InternacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescricao>()
                .HasOne(p => p.Profissional)
                .WithMany(pr => pr.Prescricoes)
                .HasForeignKey(p => p.ProfissionalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescricao>()
                .HasOne(p => p.Atendimento)
                .WithMany(a => a.Prescricoes)
                .HasForeignKey(p => p.AtendimentoId)
                .OnDelete(DeleteBehavior.Restrict);


        }

    }
}
