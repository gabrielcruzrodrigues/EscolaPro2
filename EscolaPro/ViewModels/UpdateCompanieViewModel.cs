using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels
{
    public class UpdateCompanieViewModel
    {
        public string? Name { get; set; }
        public string? ConnectionString { get; set; }
        public string? Cnpj { get; set; }
    }
}
