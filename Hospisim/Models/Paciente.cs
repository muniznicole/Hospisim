using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models
{
    public class Paciente
    {
        [Key]
        public Guid Id { get; set; }

        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string TipoSanguineo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string EnderecoCompleto { get; set; }
        public string NumeroCartaoSUS { get; set; }
        public string EstadoCivil { get; set; }
        public bool PossuiPlanoSaude { get; set; }

        public ICollection<Prontuario> Prontuarios { get; set; }
        public ICollection<Internacao> Internacoes { get; set; }
    }
}
