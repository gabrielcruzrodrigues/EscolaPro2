using EscolaPro.Enums;
using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels.Educacional
{
    public class CreateFinancialResponsibleViewModel : CreateUserInternalViewModel
    {
        [Required(ErrorMessage = "O estado civil do usuário é obrigatório.")]
        public required CivilStateEnum CivilState { get; set; }
    }
}
