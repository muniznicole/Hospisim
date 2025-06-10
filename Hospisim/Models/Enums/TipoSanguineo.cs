using System.ComponentModel.DataAnnotations;

public enum TipoSanguineo
{
    [Display(Name = "A+")]
    APositivo,

    [Display(Name = "A-")]
    ANegativo,

    [Display(Name = "B+")]
    BPositivo,

    [Display(Name = "B-")]
    BNegativo,

    [Display(Name = "AB+")]
    ABPositivo,

    [Display(Name = "AB-")]
    ABNegativo,

    [Display(Name = "O+")]
    OPositivo,

    [Display(Name = "O-")]
    ONegativo
}

