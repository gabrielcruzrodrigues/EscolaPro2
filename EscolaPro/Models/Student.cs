using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaPro.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public required long Id { get; set; }

        [Required]
        public required bool Status { get; set; }

        public ICollection<Family> Families { get; set; } = new List<Family>();
        public FixedHealth FixedHealth { get; set; }
    }
}
