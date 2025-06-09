using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Hospisim.Models
{
    public class Profissional
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }
        
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Registro do Conselho")]
        public string RegistroConselho { get; set; }

        [Display(Name = "Tipo de Registro")]
        public string TipoRegistro { get; set; }

        [Display(Name = "Especialidade")]
        public Guid EspecialidadeId { get; set; }

        [Display(Name = "Data de Admissão")]
        public DateTime DataAdmissao { get; set; }

        [Display(Name = "Carga Horária Semanal")]
        public int CargaHorariaSemanal { get; set; }

        [Display(Name = "Turno")]
        public string Turno { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        [ValidateNever]
        public Especialidade Especialidade { get; set; }

        [ValidateNever]
        public ICollection<Atendimento>? Atendimentos { get; set; }

        [ValidateNever]
        public ICollection<Prescricao>? Prescricoes { get; set; }
    }
}

