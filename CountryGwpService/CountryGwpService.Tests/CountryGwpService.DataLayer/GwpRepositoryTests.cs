using CountryGwpService.DataLayer.Services;
using CountryGwpService.DataLayer;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountryGwpService.DataLayer.Models;

namespace CountryGwpService.Tests.CountryGwpService.DataLayer
{
    [TestFixture]
    public class GwpRepositoryTests
    {
        private Mock<IDataService> _mockDataService;
        private GwpRepository _repository;

        [SetUp]
        public void Setup()
        {
            _mockDataService = new Mock<IDataService>();
            _repository = new GwpRepository(_mockDataService.Object);
        }


        [Test]
        public async Task GetAverageGwpAsync_NoMatchingData_ReturnsZeros()
        {
            // Arrange
            var csvData = new List<GwpData>();

            _mockDataService.Setup(ds => ds.CsvData).Returns(csvData);

            var country = "US";
            var lob = new List<string> { "LOB1", "LOB2" };

            var expectedAverages = new Dictionary<string, double>
            {
                { "LOB1", 0.0 },
                { "LOB2", 0.0 }
            };

            // Act
            var result = await _repository.GetAverageGwpAsync(country, lob);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAverages.Count, result.Count);

            foreach (var kvp in expectedAverages)
            {
                Assert.IsTrue(result.ContainsKey(kvp.Key));
                Assert.AreEqual(kvp.Value, result[kvp.Key]);
            }
        }

        [Test]
        public async Task GetAverageGwpAsync_SomeLobHasNoData_ReturnsZeroForThoseLobs()
        {
            // Arrange
            var csvData = new List<GwpData>
            {
                new GwpData
                {
                    Country = "US",
                    LineOfBusiness = "LOB1",
                    YearlyData = new Dictionary<int, double?>
                    {
                        { 2021, 100.0 }
                    }
                }
            };

            _mockDataService.Setup(ds => ds.CsvData).Returns(csvData);

            var country = "US";
            var lob = new List<string> { "LOB1", "LOB2" };

            var expectedAverages = new Dictionary<string, double>
            {
                { "LOB1", 100.0 },
                { "LOB2", 0.0 }
            };

            // Act
            var result = await _repository.GetAverageGwpAsync(country, lob);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAverages.Count, result.Count);

            foreach (var kvp in expectedAverages)
            {
                Assert.IsTrue(result.ContainsKey(kvp.Key));
                Assert.AreEqual(kvp.Value, result[kvp.Key]);
            }
        }
    }
}
