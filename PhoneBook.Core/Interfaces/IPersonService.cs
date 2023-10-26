using PhoneBook.Core.Models;

namespace PhoneBook.Core.Interfaces
{
    public interface IPersonService
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
