﻿// <auto-generated />
using System;
using Hospisim.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hospisim.Migrations
{
    [DbContext(typeof(HospisimContext))]
    [Migration("20250609125754_FixRestricaoPrescricao")]
    partial class FixRestricaoPrescricao
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hospisim.Models.AltaHospitalar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CondicaoPaciente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstrucoesPosAlta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("InternacaoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("InternacaoId")
                        .IsUnique();

                    b.ToTable("AltasHospitalares");
                });

            modelBuilder.Entity("Hospisim.Models.Atendimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime2");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProfissionalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProntuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProfissionalId");

                    b.HasIndex("ProntuarioId");

                    b.ToTable("Atendimentos");
                });

            modelBuilder.Entity("Hospisim.Models.Especialidade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Especialidades");
                });

            modelBuilder.Entity("Hospisim.Models.Exame", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AtendimentoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DataRealizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataSolicitacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Resultado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AtendimentoId");

                    b.ToTable("Exames");
                });

            modelBuilder.Entity("Hospisim.Models.Internacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AtendimentoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime2");

                    b.Property<string>("Leito")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotivoInternacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObservacoesClinicas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PacienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PlanoSaudeUtilizado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PrevisaoAlta")
                        .HasColumnType("datetime2");

                    b.Property<string>("Quarto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Setor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusInternacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AtendimentoId")
                        .IsUnique();

                    b.HasIndex("PacienteId");

                    b.ToTable("Internacoes");
                });

            modelBuilder.Entity("Hospisim.Models.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnderecoCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstadoCivil")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroCartaoSUS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PossuiPlanoSaude")
                        .HasColumnType("bit");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoSanguineo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Hospisim.Models.Prescricao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AtendimentoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Dosagem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Frequencia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Medicamento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProfissionalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReacoesAdversas")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusPrescricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ViaAdministracao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AtendimentoId");

                    b.HasIndex("ProfissionalId");

                    b.ToTable("Prescricoes");
                });

            modelBuilder.Entity("Hospisim.Models.Profissional", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CargaHorariaSemanal")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAdmissao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EspecialidadeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistroConselho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoRegistro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Turno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EspecialidadeId");

                    b.ToTable("Profissionais");
                });

            modelBuilder.Entity("Hospisim.Models.Prontuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataAbertura")
                        .HasColumnType("datetime2");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObservacoesGerais")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PacienteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.ToTable("Prontuarios");
                });

            modelBuilder.Entity("Hospisim.Models.AltaHospitalar", b =>
                {
                    b.HasOne("Hospisim.Models.Internacao", "Internacao")
                        .WithOne("AltaHospitalar")
                        .HasForeignKey("Hospisim.Models.AltaHospitalar", "InternacaoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Internacao");
                });

            modelBuilder.Entity("Hospisim.Models.Atendimento", b =>
                {
                    b.HasOne("Hospisim.Models.Profissional", "Profissional")
                        .WithMany("Atendimentos")
                        .HasForeignKey("ProfissionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospisim.Models.Prontuario", "Prontuario")
                        .WithMany("Atendimentos")
                        .HasForeignKey("ProntuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profissional");

                    b.Navigation("Prontuario");
                });

            modelBuilder.Entity("Hospisim.Models.Exame", b =>
                {
                    b.HasOne("Hospisim.Models.Atendimento", "Atendimento")
                        .WithMany("Exames")
                        .HasForeignKey("AtendimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atendimento");
                });

            modelBuilder.Entity("Hospisim.Models.Internacao", b =>
                {
                    b.HasOne("Hospisim.Models.Atendimento", "Atendimento")
                        .WithOne("Internacao")
                        .HasForeignKey("Hospisim.Models.Internacao", "AtendimentoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Hospisim.Models.Paciente", "Paciente")
                        .WithMany("Internacoes")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Atendimento");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Hospisim.Models.Prescricao", b =>
                {
                    b.HasOne("Hospisim.Models.Atendimento", "Atendimento")
                        .WithMany("Prescricoes")
                        .HasForeignKey("AtendimentoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Hospisim.Models.Profissional", "Profissional")
                        .WithMany("Prescricoes")
                        .HasForeignKey("ProfissionalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Atendimento");

                    b.Navigation("Profissional");
                });

            modelBuilder.Entity("Hospisim.Models.Profissional", b =>
                {
                    b.HasOne("Hospisim.Models.Especialidade", "Especialidade")
                        .WithMany("Profissionais")
                        .HasForeignKey("EspecialidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Especialidade");
                });

            modelBuilder.Entity("Hospisim.Models.Prontuario", b =>
                {
                    b.HasOne("Hospisim.Models.Paciente", "Paciente")
                        .WithMany("Prontuarios")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Hospisim.Models.Atendimento", b =>
                {
                    b.Navigation("Exames");

                    b.Navigation("Internacao")
                        .IsRequired();

                    b.Navigation("Prescricoes");
                });

            modelBuilder.Entity("Hospisim.Models.Especialidade", b =>
                {
                    b.Navigation("Profissionais");
                });

            modelBuilder.Entity("Hospisim.Models.Internacao", b =>
                {
                    b.Navigation("AltaHospitalar")
                        .IsRequired();
                });

            modelBuilder.Entity("Hospisim.Models.Paciente", b =>
                {
                    b.Navigation("Internacoes");

                    b.Navigation("Prontuarios");
                });

            modelBuilder.Entity("Hospisim.Models.Profissional", b =>
                {
                    b.Navigation("Atendimentos");

                    b.Navigation("Prescricoes");
                });

            modelBuilder.Entity("Hospisim.Models.Prontuario", b =>
                {
                    b.Navigation("Atendimentos");
                });
#pragma warning restore 612, 618
        }
    }
}
