using System.ComponentModel.DataAnnotations;

namespace EscolaPro.Models
{
    public class Companies
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string ConnectionString { get; set; }
    }
}
