using System.ComponentModel.DataAnnotations;

namespace EscolaPro.Models
{
    public class Allergie
    {
        [Key]
        public required int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "O nome da alergia é obrigatório!")]
        public required string Name { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public required bool Status { get; set; }
    }
}
