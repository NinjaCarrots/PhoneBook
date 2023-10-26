using Azure;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Core.Models;
using PhoneBook.Data.Context;
using PhoneBook.Data.Repositories.BaseInterfaces;
using PhoneBook.Data.Repositories.BaseRepository;

namespace PhoneBook.Data.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public CompanyRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return await List();
        }

        public async Task<Company> GetCompanyById(int id)
        {
            return await Get(id);
        }

        public async Task<int> GetPersonCountByCompany(int companyId)
        {
            return await _dataContext.People.CountAsync(x => x.CompanyId == companyId);
        }

        public async Task<bool> InsertCompany(Company company)
        {
            await Insert(company);
            return true;
        }

        public async Task<IEnumerable<Company>> GetPagedCompanies(int pageIndex, int pageSize)
        {
            return await Pagination(pageIndex, pageSize);
        }
    }
}
