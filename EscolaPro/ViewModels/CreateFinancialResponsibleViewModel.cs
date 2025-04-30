using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels
{
    public class CreateFinancialResponsibleViewModel
    {
        [Required]
        public required string Name { get; set; }
    }
}
