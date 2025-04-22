using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels
{
    public class UpdateAllergieViewModel
    {
        [Required(ErrorMessage = "O id da alergia é obrigatório!")]
        public required int Id { get; set; }

        public string? Name { get; set; }
    }
}
