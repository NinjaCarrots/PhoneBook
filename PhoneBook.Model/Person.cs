using PhoneBook.Core.Domain;

namespace PhoneBook.Model
{
    public class Person : BaseEntity
    {
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; } = new Company();
    }
}
