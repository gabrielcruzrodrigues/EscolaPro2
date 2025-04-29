using EscolaPro.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaPro.Models;

[Table("Families")]
public class Family : UserInternal
{
    public string? WorkAddress { get; set; }
    public string? Ocupation { get; set; }

    [Required]
    public required FamilyTypeEnum Type { get; set; }
}
