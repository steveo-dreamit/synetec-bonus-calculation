using BusinessLogic;
using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SynetecAssessmentApi.Controllers;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Persistence;
using System;
using System.Threading.Tasks;

namespace Synetech.UnitTests
{
    [TestFixture]
    public class ApiTests
    {
        private Mock<IBonusPoolService> _mockBonusPoolService;
        private Mock<AppDbContext> _mockAppDbContext;
        private BonusPoolController _bonusPoolController;
        private BonusPoolService _bonusPoolService;
        private DbContextGenerator _DbContextGenerator;

        [SetUp]
        public void Initialise()
        {
            _mockBonusPoolService = new Mock<IBonusPoolService>();
            _bonusPoolController = new BonusPoolController(_mockBonusPoolService.Object);

            _mockAppDbContext = new Mock<AppDbContext>();            
        }

        [Test]
        public async System.Threading.Tasks.Task Return_0_For_Invalid_EmployeeIdAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            var context = new AppDbContext(options);

            DbContextGenerator.SeedData(context);

            _bonusPoolService = new BonusPoolService(context);

            var check = _mockBonusPoolService.Setup(x => x.EmployeeResultAsync(It.IsAny<Decimal>(), It.IsAny<Int32>())).Returns(It.IsAny<Task<BonusPoolCalculatorResultDto>>());            

            var result = await _bonusPoolService.CalculateAsync(123456, 4);            

        }
    }
}
