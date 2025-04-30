using EscolaPro.Enums;
using System.ComponentModel.DataAnnotations;

namespace EscolaPro.Models.Educacional
{
    public class FinancialResponsible : UserInternal
    {
        [Required]
        public required CivilStateEnum CivilState { get; set; }
    }
}
