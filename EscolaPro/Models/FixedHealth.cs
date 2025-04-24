using System.ComponentModel.DataAnnotations;

namespace EscolaPro.Models;

public class FixedHealth
{
    [Key]
    public int Id { get; set; }

    [StringLength(3)]
    public string? BloodGroup { get; set; }

    [Required]
    public required int QuantityBrothers { get; set; }

    [Required]
    public required bool ToGoOutAuthorization { get; set; }

    [Required]
    public required bool Status { get; set; } = true;

    [Required]
    public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public required DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Allergie> Allergies { get; set; } = new List<Allergie>();
}
