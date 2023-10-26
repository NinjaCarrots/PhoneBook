using PhoneBook.Core.Interfaces;

namespace PhoneBook.Tests.Logic
{
    [TestClass]
    public class PersonControllerTests
    {
        [TestMethod]
        public async Task Person_Add_ReturnsCreatedResult()
        {
            //Arrange
            var mockPersonService = new Mock<IPersonService>();
            var controller = new PersonController(mockPersonService.Object);

            var newPerson = new PersonDto
            {
                FullName = "Sinethemba Mndela",
                PhoneNumber = "77401522",
                Address = "Msida",
                CompanyId = 1
            };

            mockPersonService.Setup(service => service.InsertPerson(It.IsAny<Person>())).ReturnsAsync(true);

            //Act
            var result = await controller.InsertPerson(newPerson);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult), "Result should be an CreatedAtActionResult.");
            var createdResult = (CreatedAtActionResult)result;
            Assert.AreEqual(201, createdResult.StatusCode, "Expected status code 201.");
        }

        [TestMethod]
        public async Task Person_Edit_ReturnsOkResult()
        {
            //Arrange
            var mockPersonService = new Mock<IPersonService>();
            var controller = new PersonController(mockPersonService.Object);
            var personId = 1;
            var updatedPerson = new PersonDto
            {
                Id = personId,
                FullName = "Sinethemba Mndela",
                PhoneNumber = "77401522",
                Address = "Msida",
                CompanyId = 1
            };

            mockPersonService.Setup(service => service.ViewPersonProfile(personId)).ReturnsAsync(new Person
            {
                Id = personId,
                FullName = "Sinethemba Mndela",
                PhoneNumber = "77401522",
                Address = "Msida",
                CompanyId = 1
            });

            mockPersonService.Setup(service => service.UpdatePerson(It.IsAny<Person>())).ReturnsAsync(true);

            //Act
            var result = await controller.UpdatePerson(personId, updatedPerson);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkResult), "Result should be an OkResult.");
            mockPersonService.Verify(service => service.UpdatePerson(It.IsAny<Person>()), Times.Once);
        }

        [TestMethod]
        public async Task Person_Add_ReturnsOkResult()
        {
            //Arrange
            var mockPersonService = new Mock<IPersonService>();
            var controller = new PersonController(mockPersonService.Object);
            var newPerson = new PersonDto
            {
                FullName = "Sinethemba Mndela",
                PhoneNumber = "77401522",
                Address = "Msida",
                CompanyId = 1
            };

            mockPersonService.Setup(service => service.InsertPerson(It.IsAny<Person>())).Verifiable();

            //Act
            var result = await controller.InsertPerson(newPerson);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult), "Result should be an CreatedAtActionResult.");
            mockPersonService.Verify(service => service.InsertPerson(It.IsAny<Person>()), Times.Once);
            var createdResult = (CreatedAtActionResult)result;
            Assert.AreEqual(201, createdResult.StatusCode, "Expected status code 201.");
        }

        [TestMethod]
        public async Task Person_Remove_ReturnsOkResult()
        {
            // Arrange
            var mockPersonService = new Mock<IPersonService>();
            var controller = new PersonController(mockPersonService.Object);
            var existingPerson = 1;

            mockPersonService.Setup(service => service.ViewPersonProfile(existingPerson)).ReturnsAsync(new Person
            {
                FullName = "Sinethemba Mndela",
                PhoneNumber = "77401522",
                Address = "Msida",
                CompanyId = 1
            });
            mockPersonService.Setup(service => service.DeletePerson(It.IsAny<Person>())).ReturnsAsync(true);

            // Act
            var result = await controller.DeletePerson(existingPerson);

            // Assert
            var statusCodeResult = result as StatusCodeResult;
            Assert.IsNotNull(statusCodeResult, "Result should be a StatusCodeResult.");
            Assert.AreEqual(204, statusCodeResult.StatusCode, "Expected status code 204.");

            mockPersonService.Verify(service => service.ViewPersonProfile(existingPerson), Times.Once);
            mockPersonService.Verify(service => service.DeletePerson(It.IsAny<Person>()), Times.Once);
        }

        [TestMethod]
        public async Task Person_GetAll_ReturnsListOfPersons()
        {
            // Arrange
            var mockPersonService = new Mock<IPersonService>();
            var controller = new PersonController(mockPersonService.Object);
            var people = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    FullName = "Sinethemba Mndela",
                    PhoneNumber = "77401522",
                    Address = "Msida",
                    CompanyId = 1,
                }
            };

            mockPersonService.Setup(service => service.GetAllPeople())
                .ReturnsAsync(people);

            // Act
            var result = await controller.GetAllPeople();

            // Assert
            var peopleResult = result as OkObjectResult;
            Assert.IsNotNull(peopleResult);
            Assert.AreEqual(200, peopleResult.StatusCode, "Expected status code 200.");
            var returnedPeople = peopleResult.Value as List<Person>;
            Assert.IsNotNull(returnedPeople, "Result should be a list of Person object.");
            Assert.AreEqual(people.Count, returnedPeople.Count,"Returns total number of people.");
        }

        [TestMethod]
        public async Task person_search_returnslistofmatchingpersons()
        {
            // Arrange
            var mockPersonService = new Mock<IPersonService>();
            var controller = new PersonController(mockPersonService.Object);
            var searchField = "Sinethemba";

            var matchingPeople = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    FullName = "Sinethemba",
                    PhoneNumber = "77401522",
                    Address = "Msida",
                    CompanyId = 1
                },
                new Person
                {
                    Id = 2,
                    FullName = "Mndela",
                    PhoneNumber = "739165789",
                    Address = "Malika",
                    CompanyId = 2
                },
            };

            mockPersonService.Setup(service => service.SearchByText(searchField))
                .ReturnsAsync(matchingPeople);

            // Act
            var result = await controller.SearchByText(searchField);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Result should be an OkObjectResult.");
            Assert.AreEqual(200, okResult.StatusCode, "Expected status code 200.");

            var peopleResult = okResult.Value as List<Person>;
            Assert.IsNotNull(peopleResult, "Result value should be a list of persons.");
            Assert.AreEqual(matchingPeople.Count, peopleResult.Count, "Unexpected number of matching persons.");
        }


        [TestMethod]
        public async Task Person_WildCard_ReturnsRandomPerson()
        {
            // Arrange
            var mockPersonService = new Mock<IPersonService>();
            var controller = new PersonController(mockPersonService.Object);

            var mockPersons = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    FullName = "Sinethemba",
                    PhoneNumber = "77401522",
                    Address = "Msida",
                    CompanyId = 1
                },
                new Person
                {
                    Id = 2,
                    FullName = "Mndela",
                    PhoneNumber = "739165789",
                    Address = "Malika",
                    CompanyId = 2
                }
            };

            mockPersonService.Setup(service => service.WildCardPerson())
            .ReturnsAsync(mockPersons[0]);

            // Act
            var result = await controller.WildCardPerson();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult, "Result should be an OkObjectResult.");
            Assert.AreEqual(200, okResult.StatusCode, "Expected status code 200.");

            var randomPerson = okResult.Value as Person;
            Assert.IsNotNull(randomPerson, "Result value should be a Person object.");
        }
    }
}