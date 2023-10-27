using PhoneBook.Core.Interfaces;
using PhoneBook.Core.Models;
using PhoneBook.Data.Repositories.BaseInterfaces;

namespace PhoneBook.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public CompanyService(){}

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return await _companyRepository.GetAllCompanies();
        }

        public async Task<Company> GetCompanyById(int id)
        {
            return await _companyRepository.GetCompanyById(id);
        }

        public async Task<IEnumerable<Company>> GetPagedCompanies(int pageIndex, int pageSize)
        {
            return await _companyRepository.GetPagedCompanies(pageIndex, pageSize);
        }

        public async Task<int> GetPersonCountByCompany(int companyId)
        {
            return await _companyRepository.GetPersonCountByCompany(companyId);
        }

        public async Task<bool> InsertCompany(Company company)
        {
            return await _companyRepository.InsertCompany(company);
        }

        public async Task<IEnumerable<CompanyPeopleCount>> GetCompanyWithPeopleCount()
        {
            return await _companyRepository.GetCompanyWithPeopleCount();
        }
    }
}
