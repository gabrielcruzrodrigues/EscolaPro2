using EscolaPro.Models;
using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels.Educacional;

public class UpdateFixedHealthViewModel
{
    [Required(ErrorMessage = "O Id do estudante é obrigatório!")]
    public required long StudentId { get; set; }
    public string? BloodGroup { get; set; }
    public int? QuantityBrothers { get; set; }
    public bool? ToGoOutAuthorization { get; set; }
    public List<int>? AllergiesId { get; set; }
}
