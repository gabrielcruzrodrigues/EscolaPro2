using EscolaPro.Models.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaPro.Models
{
    [NotMapped]
    public class UserInternal
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "O nome não deve conter mais de 30 caracteres!")]
        public required string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O email não deve conter mais de 100 caracteres!")]
        public string? Email { get; set; }

        public string? Identity { get; set; }

        public string? Cpf { get; set; }

        [Required]
        public required DateTime DateOfBirth { get; set; }

        public string? Nationality { get; set; }

        public string? Naturalness { get; set; }

        [Required]
        public required Sexs Sex { get; set; }

        [Required]
        public string? Cep { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? Neighborhood { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? State { get; set; }

        [Required]
        public required Roles Role { get; set; }

        [Required]
        public required bool Status { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; }

        public DateTime? LastUpdatedAt { get; set; }
    }
}
