using System.ComponentModel.DataAnnotations;

public enum TipoAtendimento
{
    [Display(Name = "Consulta")]
    Consulta,
    [Display(Name = "Emergência")]
    Emergencia,
    [Display(Name = "Internação")]
    Internacao
}
