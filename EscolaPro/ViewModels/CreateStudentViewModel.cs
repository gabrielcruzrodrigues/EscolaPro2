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
        public CreateFinancialResponsibleViewModel? FinancialResponsible { get; set; }
        public CreateFamilyViewModel? Father { get; set; }
        public CreateFamilyViewModel? Mother { get; set; }
    }
}
