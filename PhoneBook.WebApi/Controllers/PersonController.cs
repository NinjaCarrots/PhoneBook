using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Core.Interfaces;
using PhoneBook.Core.Models;
using static System.Net.Mime.MediaTypeNames;

namespace PhoneBook.WebApi.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("allpeople")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllPeople()
        {
            var people = await _personService.GetAllPeople();
            return Ok(people);
        }

        [HttpGet("search-person")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SearchByText(string search)
        {
            var person = await _personService.SearchByText(search);
            if (person != null)
                return Ok(person);
            else
                return NotFound($"Person profile with field: {search} was not found");
        }

        [HttpGet("pagination")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult GetPagedPeople([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var persons = _personService.GetPagedPeople(page, pageSize);
            return Ok(persons);
        }

        [HttpGet("get-profile")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ViewPersonProfile(int id)
        {
            var person = await _personService.ViewPersonProfile(id);
            if (person != null)
                return Ok(person);
            else
                return NotFound($"Person profile with Id: {id} was not found");
        }

        [HttpPost("add-person")]
        [ProducesResponseType(typeof(PersonDto), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> InsertPerson([FromBody] PersonDto person)
        {
            Person _person = new();
            _person.FullName = person.FullName;
            _person.PhoneNumber = person.PhoneNumber;
            _person.Address = person.Address;
            _person.CompanyId = person.CompanyId;

            await _personService.InsertPerson(_person);
            return CreatedAtAction("ViewPersonProfile", new { id = person.Id, _person});
        }

        [HttpPut("update-person")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdatePerson(int id, PersonDto person)
        {
            Person _person = await _personService.ViewPersonProfile(id);

            if (_person == null)
            {
                return NotFound($"Person with Id: {id} was not found");
            }

            _person.FullName = person.FullName;
            _person.PhoneNumber = person.PhoneNumber;
            _person.Address = person.Address;
            _person.CompanyId = person.CompanyId;

            await _personService.UpdatePerson(_person);
            return Ok();
        }

        [HttpDelete("remove")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var personExist = await _personService.ViewPersonProfile(id);
            if (personExist != null)
            {
                bool isDeleted = await _personService.DeletePerson(personExist);
                if (!isDeleted)
                    throw new Exception($"Unable to delete the person id:{id}");
                return StatusCode(204);
            }
            else
                return NotFound($"Person with Id: {id} was not found");
        }

        [HttpGet("random-profile")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> WildCardPerson()
        {
            var randomPerson = await _personService.WildCardPerson();
            if (randomPerson != null)
                return Ok(randomPerson);
            else
                return NotFound($"Random person was not found");
        }
    }
}
