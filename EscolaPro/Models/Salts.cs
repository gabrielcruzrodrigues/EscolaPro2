using System.ComponentModel.DataAnnotations;

namespace EscolaPro.Models
{
    public class Salts
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        [StringLength(256)]
        public string? SaltHash { get; set; }

        public UserGeneral UserGeneral { get; set; }
    }
}
