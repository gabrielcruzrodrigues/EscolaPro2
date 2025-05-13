using EscolaPro.Enums;
using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels.Educacional
{
    public class CreateUserInternalViewModel
    {
        public IFormFile? Image { get; set; } //

        [Required]
        [StringLength(30, ErrorMessage = "O nome não deve conter mais de 30 caracteres!")]
        public required string Name { get; set; }//

        [Required]
        [StringLength(100, ErrorMessage = "O email não deve conter mais de 100 caracteres!")]
        public required string Email { get; set; }//

        [Required]
        [StringLength(10, ErrorMessage = "O RG é obrigatório!")]
        public required string Rg { get; set; }//

        public IFormFile? RgFile { get; set; }

        public string? RgFilePath { get; set; }

        [Required(ErrorMessage = "O orgão expeditor do RG é obrigatório!")]
        [StringLength(10)]
        public required string RgDispatched { get; set; }//

        [Required(ErrorMessage = "A data de expedição do RG é obrigatório!")]
        public required DateTime RgDispatchedDate { get; set; }//

        [Required]
        [StringLength(11, ErrorMessage = "O Cpf é obrigatório!")]
        public required string Cpf { get; set; } //

        public IFormFile? CpfFile { get; set; }

        public string? RgCpfPath { get; set; }

        [Required]
        public required DateTime DateOfBirth { get; set; }//

        [Required]
        [StringLength(30, ErrorMessage = "A nacionalidade é obrigatória!")]
        public required string Nationality { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "A naturalidade é obrigatória!")]
        public required string Naturalness { get; set; }

        [Required]
        public required SexsEnum Sex { get; set; }//

        public string? Cep { get; set; }//

        public string? Address { get; set; }//

        public IFormFile? ProofOfResidenceFile { get; set; }

        public string? ProofOfResidenceFilePath { get; set; }

        [Required(ErrorMessage = "O número do imóvel é obrigatório!")]
        public required string HomeNumber { get; set; }//

        [Required]
        [StringLength(11, ErrorMessage = "O Telefone é obrigatório!")]
        public required string Phone { get; set; }//

        public string? Neighborhood { get; set; }//

        [Required]
        [StringLength(20, ErrorMessage = "A cidade é obrigatória!")]
        public required string City { get; set; }//

        [Required]
        [StringLength(2, ErrorMessage = "O estado é obrigatório!")]
        public required string State { get; set; }//

        [Required]
        public required RolesEnum Role { get; set; }//
    }
}
