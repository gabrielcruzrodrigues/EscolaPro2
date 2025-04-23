using EscolaPro.Models.Dtos;
namespace EscolaPro.ViewModels;

public class UpdateFamilyViewModel : UpdateUserInternalViewModel
{
    public string? WorkAddress { get; set; }
    public string? Ocupation { get; set; }
    public FamilyType? Type { get; set; }
    public long? StudentId { get; set; }
}
