using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaPro.Models
{
    [Table("Salts")]
    public class Salt
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public long UserGeneralId { get; set; }

        [Required]
        [StringLength(256)]
        public string? SaltHash { get; set; }

        public UserGeneral UserGeneral { get; set; }
    }
}
