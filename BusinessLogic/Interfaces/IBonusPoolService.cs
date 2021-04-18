using SynetecAssessmentApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IBonusPoolService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        
        // Task<decimal?> CalculateAsync(decimal bonusPoolAmount, int selectedEmployeeId);
        Task<BonusPoolCalculatorResultDto> CalculateAsync(decimal bonusPoolAmount, int selectedEmployeeId);
        
    }
}
