using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Dtos;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class BonusPoolController : Controller
    {
        private readonly IBonusPoolService _bonusPoolService;        

        public BonusPoolController(IBonusPoolService bonusPoolService)
        {
            _bonusPoolService = bonusPoolService;            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bonusPoolService.GetEmployeesAsync();
            
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            if (request.SelectedEmployeeId < 1)
            {
                ModelState.AddModelError(nameof(CalculateBonusDto.SelectedEmployeeId), "The SelectedEmployeeId value must be greater than 0");
                return BadRequest(ModelState);
            }

            var result = await _bonusPoolService.CalculateAsync(request.TotalBonusPoolAmount, request.SelectedEmployeeId);            

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }            
        }
    }
}
