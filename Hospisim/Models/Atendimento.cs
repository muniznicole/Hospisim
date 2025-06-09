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
        public string Tipo { get; set; } // emergência, consulta, internação
        public string Status { get; set; } // realizado, em andamento, cancelado
        public string Local { get; set; } // exemplo: sala 01

        public Guid ProntuarioId { get; set; }
        public Prontuario Prontuario { get; set; }

        public Guid ProfissionalId { get; set; }
        public Profissional Profissional { get; set; }

        public ICollection<Prescricao> Prescricoes { get; set; }
        public ICollection<Exame> Exames { get; set; }

        public Internacao Internacao { get; set; }
    }
}

