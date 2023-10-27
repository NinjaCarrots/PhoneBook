using Azure;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Core.Models;
using PhoneBook.Data.Context;
using PhoneBook.Data.Repositories.BaseInterfaces;
using PhoneBook.Data.Repositories.BaseRepository;
using System.Transactions;

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
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await Insert(company);
                    transactionScope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Company>> GetPagedCompanies(int pageIndex, int pageSize)
        {
            return await Pagination(pageIndex, pageSize);
        }

        public async Task<IEnumerable<CompanyPeopleCount>> GetCompanyWithPeopleCount()
        {
            var peopleCount = await _dataContext.Company
            .Select(x => new CompanyPeopleCount
            {
                Id = x.Id,
                CompanyName = x.CompanyName,
                RegistrationDate = x.RegistrationDate,
                TotalNumberOfPeople = x.People.Count()
            })
            .ToListAsync();
            return peopleCount;
        }
    }
}
