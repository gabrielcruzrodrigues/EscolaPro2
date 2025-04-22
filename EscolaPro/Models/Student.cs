using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaPro.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public required long Id { get; set; }




        public ICollection<Family> Families { get; set; } = new List<Family>();
    }
}
