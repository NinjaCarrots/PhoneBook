using PhoneBook.Core.Models;

namespace PhoneBook.Data.Repositories.BaseInterfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<IEnumerable<Person>> GetAllPeople();
        Task<IEnumerable<Person>> SearchByText(string searchField);
        Task<IEnumerable<Person>> GetPagedPeople(int pageIndex, int pageSize);
        Task<Person> ViewPersonProfile(int id);
        Task<bool> InsertPerson(Person person);
        Task<bool> UpdatePerson(Person person);
        Task<bool> DeletePerson(Person person);
        Task<Person> WildCardPerson();
    }
}
