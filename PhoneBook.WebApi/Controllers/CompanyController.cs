using Microsoft.AspNetCore.Mvc;
using PhoneBook.Core.Interfaces;
using PhoneBook.Core.Models;
using System;

namespace PhoneBook.WebApi.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("get-company")]
        [ProducesResponseType(typeof(Company), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _companyService.GetCompanyById(id);
            if (company != null)
                return Ok(company);
            else
                return NotFound($"Company was not found");
        }

        [HttpGet("company-list")]
        [ProducesResponseType(typeof(Company), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _companyService.GetAllCompanies();
            return Ok(companies);
        }

        [HttpGet("pagination")]
        [ProducesResponseType(typeof(Company), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult GetPagedCompany([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var companies = _companyService.GetPagedCompanies(page, pageSize);
            return Ok(companies);
        }


        [HttpGet("count-people")]
        [ProducesResponseType(typeof(Company), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPersonCountByCompany(int id)
        {
            var person = await _companyService.GetPersonCountByCompany(id);
            if (person != 0)
                return Ok(person);
            else
                return NotFound($"Person count was not found");
        }

        [HttpPost("add-company")]
        [ProducesResponseType(typeof(Company), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> InsertCompany([FromBody] CompanyDto company)
        {
            Company _company = new();
            _company.CompanyName = company.CompanyName;
            _company.RegistrationDate = company.RegistrationDate;

            await _companyService.InsertCompany(_company);
            return CreatedAtAction("GetCompany", new { id = company.CompanyId }, _company);
        }

        [HttpGet("company-peoplecount")]
        [ProducesResponseType(typeof(CompanyPeopleCount), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCompanyWithPeopleCount()
        {
            var peopleCount = await _companyService.GetCompanyWithPeopleCount();
            return Ok(peopleCount);
        }
    }
}
