using EscolaPro.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscolaPro.Models.Educacional
{
    [Table("Students")]
    public class Student : UserInternal
    {
        public long? FinancialResponsibleId { get; set; }
        public long? FatherId { get; set; }
        public long? MotherId { get; set; }

        [Required]
        public required StudentSituationEnum Situation { get; set; }
        public FixedHealth FixedHealth { get; set; } = null!;

        [ForeignKey("FinancialResponsibleId")]
        public FinancialResponsible FinancialResponsible { get; set; }

        [ForeignKey("FatherId")]
        public Family Father { get; set; }

        [ForeignKey("MotherId")]
        public Family Mother { get; set; }
    }
}
