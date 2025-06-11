using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class Prontuario
    {
        [Key]
        public Guid Id { get; set; }

        [ValidateNever]
        public string? Numero { get; set; }
        public DateTime DataAbertura { get; set; }
        public string ObservacoesGerais { get; set; }

        public Guid PacienteId { get; set; }

        [ValidateNever]
        public Paciente? Paciente { get; set; }

        public ICollection<Atendimento>? Atendimentos { get; set; }
    }
}

