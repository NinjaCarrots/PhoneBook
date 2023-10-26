using PhoneBook.Core.Interfaces;
using PhoneBook.Core.Models;
using PhoneBook.Data.Repositories.BaseInterfaces;

namespace PhoneBook.Service
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<bool> DeletePerson(Person person)
        {
            return await _personRepository.DeletePerson(person);
        }

        public async Task<IEnumerable<Person>> GetAllPeople()
        {
            return await _personRepository.GetAllPeople();
        }

        public async Task<IEnumerable<Person>> GetPagedPeople(int pageIndex, int pageSize)
        {
            return await _personRepository.GetPagedPeople(pageIndex, pageSize);
        }

        public async Task<bool> InsertPerson(Person person)
        {
            return await _personRepository.InsertPerson(person);
        }

        public async Task<IEnumerable<Person>> SearchByText(string searchField)
        {
            return await _personRepository.SearchByText(searchField);
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            return await _personRepository.UpdatePerson(person);
        }

        public async Task<Person> ViewPersonProfile(int id)
        {
            return await _personRepository.ViewPersonProfile(id);
        }

        public async Task<Person> WildCardPerson()
        {
            return await _personRepository.WildCardPerson();
        }
    }
}
