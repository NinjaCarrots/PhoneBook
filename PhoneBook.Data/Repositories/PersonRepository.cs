using Microsoft.EntityFrameworkCore;
using PhoneBook.Core.Models;
using PhoneBook.Data.Context;
using PhoneBook.Data.Repositories.BaseInterfaces;
using PhoneBook.Data.Repositories.BaseRepository;

namespace PhoneBook.Data.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public PersonRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> DeletePerson(Person person)
        {
            await Delete(person);
            return true;
        }

        public async Task<IEnumerable<Person>> GetAllPeople()
        {
            return await List();
        }

        public async Task<IEnumerable<Person>> GetPagedPeople(int pageIndex, int pageSize)
        {
            return await Pagination(pageIndex, pageSize);
        }

        public async Task<bool> InsertPerson(Person person)
        {
            await Insert(person);
            return true;
        }

        public async Task<IEnumerable<Person>> SearchByText(string searchField)
        {
            var searchByText = _dataContext.People.Where(x => x.FullName == searchField || x.PhoneNumber == searchField || x.Address == searchField || x.Company.CompanyName == searchField);
            if (searchByText != null)
            {
                return await searchByText.ToListAsync();
            }
            else
                return null;
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            await Update(person);
            return true;
        }

        public async Task<Person> ViewPersonProfile(int id)
        {
            return await Get(id);
        }

        public async Task<Person> WildCardPerson()
        {
            var random = new Random();
            var count = _dataContext.People.Count();
            var randomIndex = random.Next(0, count);
            return await _dataContext.People.Skip(randomIndex).FirstOrDefaultAsync();
        }
    }
}
