using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels.Educacional;

public class CreateCompanieViewModel
{
    [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
    public required string Name { get; set; }
    [Required(ErrorMessage = "A string de conexão é obrigatória.")]
    public required string ConnectionString { get; set; }
    [Required(ErrorMessage = "O CNPJ é obrigatório.")]
    public required string Cnpj { get; set; }
}
