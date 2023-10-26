using PhoneBook.Core.Domain;
using System;

namespace PhoneBook.Model
{
    public class Company : BaseEntity
    {
        public string? CompanyName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<Person> Persons { get; set; } = new List<Person>();
    }
}
