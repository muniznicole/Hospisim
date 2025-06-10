using System.ComponentModel.DataAnnotations;

public enum EstadoCivil
{
    [Display(Name = "Solteiro")]
    Solteiro,

    [Display(Name = "Casado")]
    Casado,

    [Display(Name = "Divorciado")]
    Divorciado,

    [Display(Name = "Viúvo")]
    Viuvo,

    [Display(Name = "União Estável")]
    UniaoEstavel
}
