using System;
using System.ComponentModel.DataAnnotations;
using Hospisim.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hospisim.Models
{
    public class Internacao
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PacienteId { get; set; }

        [ValidateNever]
        public Paciente Paciente { get; set; }

        public Guid AtendimentoId { get; set; }

        [ValidateNever]
        public Atendimento Atendimento { get; set; }

        [Display(Name = "Data de Entrada")]
        public DateTime DataEntrada { get; set; }

        [Display(Name = "Previsão de Alta")]
        public DateTime? PrevisaoAlta { get; set; }

        [Display(Name = "Motivo da Internação")]
        public string MotivoInternacao { get; set; }
        public string Leito { get; set; }
        public string Quarto { get; set; }
        public string Setor { get; set; }

        [Display(Name = "Plano de Saúde Utilizado")]
        public string? PlanoSaudeUtilizado { get; set; }

        [Display(Name = "Observações Clínicas")]
        public string ObservacoesClinicas { get; set; }

        [Display(Name = "Status da Internação")]
        public StatusInternacao StatusInternacao { get; set; } = StatusInternacao.Ativa; // Ativa, Alta concedida, Transferido, Óbito

        [ValidateNever]
        public AltaHospitalar AltaHospitalar { get; set; }
    }
}

