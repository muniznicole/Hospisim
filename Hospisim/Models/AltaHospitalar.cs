using System;
using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class AltaHospitalar
    {
        [Key]
        public Guid Id { get; set; }

        public Guid InternacaoId { get; set; }
        public Internacao? Internacao { get; set; }

        public DateTime Data { get; set; }
        public string CondicaoPaciente { get; set; }
        public string InstrucoesPosAlta { get; set; }

    }
}

