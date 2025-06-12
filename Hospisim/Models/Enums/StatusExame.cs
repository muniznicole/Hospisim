using System.ComponentModel.DataAnnotations;

namespace Hospisim.Models.Enums
{
    public enum StatusExame
    {
        [Display(Name = "Solicitado")]
        Solicitado,
        [Display(Name = "Em Andamento")]
        EmAndamento,
        [Display(Name = "Realizado")]
        Realizado,
        [Display(Name = "Cancelado")]
        Cancelado
    }
}