using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class Especialidade
    {
        [Key]
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public ICollection<Profissional> Profissionais { get; set; }
    }
}

