using EscolaPro.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaPro.Models
{
    [Table("Students")]
    public class Student : UserInternal
    {
        [Required]
        [StringLength(50)]
        public required string ResponsibleEmail { get; set; }

        public long? FinancialResponsibleId { get; set; }
        public long? FatherId { get; set; }
        public long? MotherId { get; set; }

        [Required]
        public required StudentSituationEnum Situation { get; set; }

        public ICollection<Family> Families { get; set; } = new List<Family>();
        public FixedHealth FixedHealth { get; set; } = null!;

        [ForeignKey("FinancialResponsibleId")]
        public FinancialResponsible FinancialResponsible { get; set; }

        [ForeignKey("FatherId")]
        public Family Father { get; set; }
         
        [ForeignKey("MotherId")]
        public Family Mother { get; set; }
    }
}
