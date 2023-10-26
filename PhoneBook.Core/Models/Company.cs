using PhoneBook.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Core.Models
{
    public class Company : BaseEntity
    {
        [Required]
        public string CompanyName { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public virtual ICollection<Person> People { get; set; }
    }
}
