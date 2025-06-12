using System;
using System.ComponentModel.DataAnnotations;
using Hospisim.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hospisim.Models
{
    public class Exame
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Tipo de Exame")]
        public string Tipo { get; set; } // sangue, imagem, etc.

        [Display(Name = "Data da Solicitação")]
        public DateTime DataSolicitacao { get; set; }

        [Display(Name = "Data da Realização")]
        public DateTime? DataRealizacao { get; set; }

        [Display(Name = "Resultado do Exame")]
        public string Resultado { get; set; }

        public Guid AtendimentoId { get; set; }

        [ValidateNever]
        public Atendimento Atendimento { get; set; }

        [Display(Name = "Status do Exame")]
        public StatusExame StatusExame { get; set; } 
    }
}

