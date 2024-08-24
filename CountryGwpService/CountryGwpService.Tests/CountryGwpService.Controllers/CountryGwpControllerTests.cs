using CountryGwpService.Controllers;
using CountryGwpService.DataLayer;
using CountryGwpService.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryGwpService.Tests.CountryGwpService.Controllers
{
    [TestFixture]
    public class CountryGwpControllerTests
    {
        private Mock<IGwpRepository> _mockGwpRepository;
        private CountryGwpController _controller;

        [SetUp]
        public void Setup()
        {
            _mockGwpRepository = new Mock<IGwpRepository>();
            _controller = new CountryGwpController(_mockGwpRepository.Object);
        }

       
        [Test]
        public async Task GetAverageGwp_EmptyLob_ReturnsBadRequest()
        {
            // Arrange
            var request = new GwpRequest
            {
                Country = "",
                Lob = new List<string> { "property" }
            };

            _controller.ModelState.AddModelError("Country", "Country is required");

            // Act
            var result = await _controller.GetAverageGwp(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);


        }

        [Test]
        public async Task GetAverageGwp_ValidRequest_ReturnsOkResult_WithExpectedResult()
        {
            // Arrange
            var request = new GwpRequest
            {
                Country = "US",
                Lob = new List<string> { "LOB1", "LOB2" }
            };
            var expectedResult = 123.45; // Replace with the expected result type
            _mockGwpRepository.Setup(repo => repo.GetAverageGwpAsync(It.IsAny<string>(), It.IsAny<List<string>>())).ReturnsAsync(new Dictionary<string, double>());


            // Act
            var result = await _controller.GetAverageGwp(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
