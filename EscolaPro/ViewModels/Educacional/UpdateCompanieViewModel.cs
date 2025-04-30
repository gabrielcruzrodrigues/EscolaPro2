using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels.Educacional
{
    public class UpdateCompanieViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? ConnectionString { get; set; }
        public string? Cnpj { get; set; }
    }
}
