using EscolaPro.Models;
using System.ComponentModel.DataAnnotations;

namespace EscolaPro.ViewModels.Educacional
{
    public class CreateStudentViewModel : CreateUserInternalViewModel
    {
        public required long ResponsibleId { get; set; }
        public long? FatherId { get; set; }
        public long? MotherId { get; set; }
    }
}
