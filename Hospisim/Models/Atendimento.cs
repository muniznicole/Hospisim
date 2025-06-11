using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Hospisim.Models
{
    public class Atendimento
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DataHora { get; set; }
        public TipoAtendimento Tipo { get; set; } // emergência, consulta, internação
        public StatusAtendimento Status { get; set; } // realizado, em andamento, cancelado
        public string Local { get; set; } // exemplo: sala 01

        public Guid ProntuarioId { get; set; }

        [ValidateNever]
        public Prontuario? Prontuario { get; set; }

        public Guid ProfissionalId { get; set; }
        
        [ValidateNever]
        public Profissional? Profissional { get; set; }

        [ValidateNever]
        public ICollection<Prescricao>? Prescricoes { get; set; }
        
        [ValidateNever]
        public ICollection<Exame>? Exames { get; set; }

        [ValidateNever]
        public Internacao? Internacao { get; set; }
    }
}

