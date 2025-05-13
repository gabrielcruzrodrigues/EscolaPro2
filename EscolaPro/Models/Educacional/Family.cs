using EscolaPro.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaPro.Models.Educacional;

[Table("Families")]
public class Family : UserInternal
{
    [Required]
    public required FamilyTypeEnum Type { get; set; }
}
