using Microsoft.EntityFrameworkCore;
using PhoneBook.Core.Models;
using System.ComponentModel.Design;
using System.Net;

namespace PhoneBook.Data.Context
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }
        public DbSet<Company> Company { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Company Seed

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyName).IsRequired();
            });
            modelBuilder.Entity<Company>().HasData(new Company
            {
                Id = 1,
                CompanyName = "Others",
                RegistrationDate = DateTime.Now
            });
            modelBuilder.Entity<Company>().HasData(new Company
            {
                Id = 2,
                CompanyName = "Microsoft",
                RegistrationDate = DateTime.Now
            });

            #endregion Company Seed

            #region Person Seed

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.FullName).IsRequired();
                entity.Property(e => e.CompanyId).IsRequired();

                entity.HasOne(p => p.Company)
                .WithMany(c => c.People)
                .HasForeignKey(p => p.CompanyId);
            });
            modelBuilder.Entity<Person>().HasData(new Person
            {
                Id = 1,
                FullName = "Sinethemba Mndela",
                PhoneNumber = "77401522",
                Address = "Msida",
                CompanyId = 1
            });

            #endregion Person Seed
        }
    }
}
