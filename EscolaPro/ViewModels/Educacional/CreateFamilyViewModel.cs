using EscolaPro.Enums;
using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels.Educacional
{
    public class CreateFamilyViewModel : CreateUserInternalViewModel
    {
        [Required(ErrorMessage = "O tipo do familiar é obrigatório!")]
        public required FamilyTypeEnum Type { get; set; }
    }
}
