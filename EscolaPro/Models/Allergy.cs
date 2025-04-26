using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EscolaPro.Models;

public class Allergy
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(30, ErrorMessage = "O nome da alergia é obrigatório!")]
    public required string Name { get; set; }

    [Required]
    public required DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [Required]
    public required bool Status { get; set; }

    [JsonIgnore]
    public ICollection<FixedHealth> FixedHealths { get; set; } = new List<FixedHealth>();
}
