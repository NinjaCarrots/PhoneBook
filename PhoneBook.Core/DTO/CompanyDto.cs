using PhoneBook.Core.Domain;

namespace PhoneBook.Core.Models
{
    public class CompanyDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
