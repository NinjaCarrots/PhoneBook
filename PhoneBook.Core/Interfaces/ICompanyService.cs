
using PhoneBook.Core.Models;

namespace PhoneBook.Core.Interfaces
{
    public interface ICompanyService
    {
        Task<Company> GetCompanyById(int id);
        Task<bool> InsertCompany(Company company);
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<IEnumerable<Company>> GetPagedCompanies(int pageIndex, int pageSize);
        Task<int> GetPersonCountByCompany(int companyId);
        Task<IEnumerable<CompanyPeopleCount>> GetCompanyWithPeopleCount();
    }
}
