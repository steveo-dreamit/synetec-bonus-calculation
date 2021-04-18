using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly AppDbContext _dbContext;

        public BonusPoolService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            IEnumerable<Employee> employees = await _dbContext
                .Employees
                .Include(e => e.Department)
                .ToListAsync();

            List<EmployeeDto> result = new();

            foreach (var employee in employees)
            {
                result.Add(
                    new EmployeeDto
                    {
                        Fullname = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        Department = new DepartmentDto
                        {
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    });
            }

            return result;
        }        

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(decimal bonusPoolAmount, int selectedEmployeeId)
        {
            //load the details of the selected employee using the Id
            Employee employee = await _dbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == selectedEmployeeId);

            if (employee != null)
            {
                //get the total salary budget for the company
                decimal totalSalary = _dbContext.Employees.Sum(item => item.Salary);

                //calculate the bonus allocation for the employee check for divide by 0
                //return ((totalSalary > 0 ? employee.Salary / totalSalary : 0) * bonusPoolAmount);

                return new BonusPoolCalculatorResultDto
                {
                    Amount = ((totalSalary > 0 ? employee.Salary / totalSalary : 0) * bonusPoolAmount)
                };
            }
            else
            {
                return null;
            }
        }
    }
}
