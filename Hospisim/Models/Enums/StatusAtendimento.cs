using System.ComponentModel.DataAnnotations;

public enum StatusAtendimento
{
    [Display(Name = "Em andamento")]
    EmAndamento,
    [Display(Name = "Finalizado")]
    Finalizado,
    [Display(Name = "Cancelado")]
    Cancelado
}

