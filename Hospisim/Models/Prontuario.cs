using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class Prontuario
    {
        [Key]
        public Guid Id { get; set; }

        public string Numero { get; set; }
        public DateTime DataAbertura { get; set; }
        public string ObservacoesGerais { get; set; }

        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public ICollection<Atendimento> Atendimentos { get; set; }
    }
}

