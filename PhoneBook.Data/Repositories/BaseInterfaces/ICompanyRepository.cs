
using PhoneBook.Core.Models;

namespace PhoneBook.Data.Repositories.BaseInterfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> GetCompanyById(int id);
        Task<bool> InsertCompany(Company company);
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<IEnumerable<Company>> GetPagedCompanies(int pageIndex, int pageSize);
        Task<int> GetPersonCountByCompany(int companyId);
        Task<IEnumerable<CompanyPeopleCount>> GetCompanyWithPeopleCount();

    }
}
