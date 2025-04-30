using EscolaPro.Models.Educacional;
using EscolaPro.ViewModels;

namespace EscolaPro.Repositories.Interfaces.Educacional
{
    public interface IFinancialResponsibleRepository
    {
        Task<FinancialResponsible> CreateAsync(string companieName, FinancialResponsible request);
        Task<FinancialResponsible> GetByIdAsync(string companieName, long id);
        Task<IEnumerable<FinancialResponsible>> GetAllAsync(string companieName);
        Task<FinancialResponsible> GetByNameAsync(string companieName, string financialResponsibleName);
        Task<FinancialResponsible> GetByEmailAsync(string companieName, string financialResponsibleEmail);
        Task<FinancialResponsible> GetByRgAsync(string companieName, string financialResponsibleRg);
        Task<FinancialResponsible> GetByCpfAsync(string companieName, string financialResponsibleCpf);
        Task<FinancialResponsible> GetByPhoneAsync(string companieName, string financialResponsiblePhone);
        Task Update(string companieName, FinancialResponsible financialResponsibleForUpdate);
        Task Disable(string companieName, long financialResponsibleId);
        Task<IEnumerable<FinancialResponsible>> Search(string companieName, string param);
    }
}
