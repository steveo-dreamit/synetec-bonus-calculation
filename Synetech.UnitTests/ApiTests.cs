using BusinessLogic;
using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Synetech.UnitTests
{
    [TestFixture]
    public class ApiTests
    {
        private Mock<IBonusPoolService> _mockBonusPoolService;                
        private BonusPoolService _bonusPoolService;

        private DbContextOptions<AppDbContext> options;

        [SetUp]
        public void Initialise()
        {
            _mockBonusPoolService = new Mock<IBonusPoolService>();            

            options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            var context = new AppDbContext(options);

            DbContextGenerator.SeedData(context);

            _bonusPoolService = new BonusPoolService(context);
        }

        [Test]
        public async Task Returns_Valid_Result_EmployeeIdAsync()
        {
            // Arrange
            var check = _mockBonusPoolService.Setup(x => x.CalculateAsync(It.IsAny<Decimal>(), It.IsAny<Int32>())).Returns(It.IsAny<Task<BonusPoolCalculatorResultDto>>());

            // Act
            var result = await _bonusPoolService.CalculateAsync(123456, 4);
            
            // Assert
            Assert.IsNotNull(result);

            // Remember its up to the calling function to return an integer value, its not up to the api to put contraints on the significant digits!
            // but we need a test!            
            Assert.AreEqual(result.Amount.ToString("0.00"), "10370.49");
        }

        [Test]
        public async Task Returns_Null_Result_ForMissingEmployeeIdAsync()
        {
            // Arrange
            var check = _mockBonusPoolService.Setup(x => x.CalculateAsync(It.IsAny<Decimal>(), It.IsAny<Int32>())).Returns(It.IsAny<Task<BonusPoolCalculatorResultDto>>());

            // Act
            var result = await _bonusPoolService.CalculateAsync(123456, 0);

            // Assert
            Assert.AreEqual(result, null);
        }

        [Test]
        public async Task Returns_Null_Result_ForInvalidSalaryTotalAsync()
        {
            // Arrange
            var check = _mockBonusPoolService.Setup(x => x.CalculateAsync(It.IsAny<Decimal>(), It.IsAny<Int32>())).Returns(It.IsAny<Task<BonusPoolCalculatorResultDto>>());

            // Act
            var result = await _bonusPoolService.CalculateAsync(0, 0);

            // Assert
            Assert.AreEqual(result, null);
        }

        [Test]
        public async Task Returns_ResultsForGetEmployeesAsync()
        {
            // Arrange
            var check = _mockBonusPoolService.Setup(x => x.GetEmployeesAsync()).Returns(It.IsAny<Task<IEnumerable<EmployeeDto>>>());

            // Act
            var result = await _bonusPoolService.GetEmployeesAsync();

            // Assert
            Assert.IsNotNull(result);
        }        
    }
}