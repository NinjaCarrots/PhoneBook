using PhoneBook.Core.Domain;

namespace PhoneBook.Core.Models
{
    public class CompanyPeopleCount : BaseEntity
    {
        public string CompanyName { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public int TotalNumberOfPeople { get; set; }
    }
}
