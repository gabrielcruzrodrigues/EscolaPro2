using EscolaPro.Models.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaPro.Models;

[Table("Families")]
public class Family : UserInternal
{
    public string? WorkAddress { get; set; }
    public string? Ocupation { get; set; }
    public string? UF { get; set; }
    public long StudentId { get; set; }

    [Required]
    public required FamilyType Type { get; set; }

    public Student Student { get; set; }
}
