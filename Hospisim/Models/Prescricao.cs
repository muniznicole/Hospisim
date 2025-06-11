using Hospisim.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class Prescricao
    {
        [Key]
        public Guid Id { get; set; }

        public Guid AtendimentoId { get; set; }
        public Atendimento? Atendimento { get; set; }

        public Guid ProfissionalId { get; set; }
        public Profissional? Profissional { get; set; }

        public string Medicamento { get; set; }
        public string Dosagem { get; set; }       // Ex: 500mg
        public string Frequencia { get; set; }    // Ex: 8 em 8h
        public string ViaAdministracao { get; set; } // Oral, intravenosa, etc.
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Observacoes { get; set; }
        public StatusPrescricao StatusPrescricao { get; set; } // Ativa, suspensa, encerrada
        public string? ReacoesAdversas { get; set; }
    }
}
