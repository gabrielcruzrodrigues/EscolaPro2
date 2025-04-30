using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaPro.Models.Educacional;

public class FixedHealth
{
    [Key]
    public long Id { get; set; }

    [StringLength(3)]
    public string? BloodGroup { get; set; }

    [Required]
    public required int QuantityBrothers { get; set; }

    [Required]
    public required bool ToGoOutAuthorization { get; set; }

    [Required]
    public required long StudentId { get; set; }

    [Required]
    public required bool Status { get; set; } = true;

    [Required]
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public required DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    public ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
    public Student Student { get; set; } = null!;
}
