using EscolaPro.Models;
using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels
{
    public class CreateStudentViewModel : CreateUserInternalViewModel
    {
        [Required]
        public required string ResponsibleEmail { get; set; }
        public long? ResponsibleId { get; set; }
        public long? FatherId { get; set; }
        public long? MotherId { get; set; }
        public FinancialResponsible? FinancialResponsible { get; set; }
        public Family? Father { get; set; }
        public Family? Mother { get; set; }
    }
}
