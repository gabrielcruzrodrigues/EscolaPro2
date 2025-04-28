using EscolaPro.Enums;
using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels;

public class UpdateUserInternalViewModel
{
    [Required(ErrorMessage = "O id para atualizar um usuário interno é obrigatório!")]
    public required long Id { get; set; }
    public string? Image { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Rg { get; set; }
    public string? Cpf { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    public string? Naturalness { get; set; }
    public SexsEnum? Sex { get; set; }
    public string? Cep { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public RolesEnum? Role { get; set; }
}
