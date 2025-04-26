using EscolaPro.Models;
using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels
{
    public class CreateFixedHealthViewModel
    {
        [Required(ErrorMessage = "O Id do estudante é obrigatório!")]
        public required long StudentId { get; set; }

        [StringLength(3, ErrorMessage = "O tipo sanguíneo é obrigatório!")]
        public string? BloodGroup { get; set; }

        [Required(ErrorMessage = "A quantidade de irmãos é obrigatório!")]
        public required int QuantityBrothers { get; set; }

        [Required(ErrorMessage = "A verificaçao de saída é obrigatória!")]
        public required bool ToGoOutAuthorization { get; set; }
        public List<int>? AllergiesId { get; set; }
    }
}
