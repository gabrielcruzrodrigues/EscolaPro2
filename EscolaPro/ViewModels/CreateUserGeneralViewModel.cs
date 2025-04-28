using EscolaPro.Enums;
using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels;

public class CreateUserGeneralViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public required string Name { get; set; }

    [EmailAddress(ErrorMessage = "Email inválido!")]
    [Required(ErrorMessage = "O email é obrigatório!")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória!")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "A role é obrigatória!")]
    public required RolesEnum Role { get; set; }

    [Required(ErrorMessage = "A empresa é obrigatório!")]
    public required int CompanieId { get; set; }
}
