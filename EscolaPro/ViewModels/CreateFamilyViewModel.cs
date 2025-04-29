using EscolaPro.Enums;
using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels
{
    public class CreateFamilyViewModel : CreateUserInternalViewModel
    {
        public string? WorkAddress { get; set; }

        [Required(ErrorMessage = "A ocupação do familiar é obrigatória!")]
        public required string Ocupation { get; set; }

        [Required(ErrorMessage = "O tipo do familiar é obrigatório!")]
        public required FamilyTypeEnum Type { get; set; }
    }
}
