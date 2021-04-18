using SynetecAssessmentApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IBonusPoolService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<decimal> CalculateAsync(decimal bonusPoolAmount, int selectedEmployeeId);
        Task<BonusPoolCalculatorResultDto> EmployeeResultAsync(decimal bonusPoolAmount, int selectedEmployeeId);
    }
}
