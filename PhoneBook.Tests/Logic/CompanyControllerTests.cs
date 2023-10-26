using PhoneBook.Core.Interfaces;

namespace PhoneBook.Tests
{
    [TestClass]
    public class CompanyControllerTests
    {
        [TestMethod]
        public async Task Company_Add_ReturnsCreatedResult()
        {
            //Arrange
            var mockCompanyService = new Mock<ICompanyService>();
            var controller = new CompanyController(mockCompanyService.Object);
            var newCompany = new CompanyDto
            {
                CompanyName = "Company A",
                RegistrationDate = DateTime.Now,
            };

            mockCompanyService.Setup(service => service.InsertCompany(It.IsAny<Company>())).ReturnsAsync(true);


            //Act
            var result = await controller.InsertCompany(newCompany);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult), "Result should be a CreatedAtActionResult.");
            mockCompanyService.Verify(service => service.InsertCompany(It.IsAny<Company>()), Times.Once);
            var createdResult = (CreatedAtActionResult)result;
            Assert.AreEqual(201, createdResult.StatusCode, "Expected status code 201.");
        }

        [TestMethod]
        public async Task Company_GetAll_ReturnsListOfCompanies()
        {
            // Arrange
            var mockCompanyService = new Mock<ICompanyService>();
            var controller = new CompanyController(mockCompanyService.Object);
            var companies = new List<Company>
            {
                new Company
                {
                    Id = 1,
                    CompanyName = "Company A",
                    RegistrationDate = DateTime.Now,
                }
            };

            mockCompanyService.Setup(service => service.GetAllCompanies())
                .ReturnsAsync(companies);

            // Act
            var result = await controller.GetAllCompanies();

            // Assert
            var companiesResult = result as OkObjectResult;
            Assert.IsNotNull(companiesResult, "Result should be an OkObjectResult.");
            Assert.AreEqual(200, companiesResult.StatusCode, "Expected status code 200.");
            var returnedCompanies = companiesResult.Value as List<Company>;
            Assert.IsNotNull(returnedCompanies, "Result should be a list of Company objects.");
            Assert.AreEqual(companies.Count, returnedCompanies.Count, "Returns total number of companies.");
        }
    }
}