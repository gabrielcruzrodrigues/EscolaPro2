using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels
{
    public class CreateAllergieViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "O nome da alergia é obrigatório!")]
        public required string Name { get; set; }
    }
}
