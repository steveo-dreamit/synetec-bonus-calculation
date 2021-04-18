

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

            List<EmployeeDto> result = new List<EmployeeDto>();

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

        // I'll keep the original method pushed to source control and renamed it and made it private, this code would be useful
        // ** but the brief said only return amount this is then CalculateAsync!
        public async Task<BonusPoolCalculatorResultDto> EmployeeResultAsync(decimal bonusPoolAmount, int selectedEmployeeId)
        {
            //load the details of the selected employee using the Id
            Employee employee = await _dbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == selectedEmployeeId);

            //get the total salary budget for the company
            decimal totalSalary = (decimal)_dbContext.Employees.Sum(item => item.Salary);

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = employee.Salary / totalSalary;
            decimal bonusAllocation = (bonusPercentage * bonusPoolAmount);

            return new BonusPoolCalculatorResultDto
            {
                Employee = new EmployeeDto
                {
                    Fullname = employee.Fullname,
                    JobTitle = employee.JobTitle,
                    Salary = employee.Salary,
                    Department = new DepartmentDto
                    {
                        Title = employee.Department.Title,
                        Description = employee.Department.Description
                    }
                },

                Amount = bonusAllocation
            };
        }

        public async Task<decimal> CalculateAsync(decimal bonusPoolAmount, int selectedEmployeeId)
        {
            //load the details of the selected employee using the Id
            Employee employee = await _dbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == selectedEmployeeId);

            //get the total salary budget for the company
            decimal totalSalary = (decimal)_dbContext.Employees.Sum(item => item.Salary);

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = employee.Salary / totalSalary;
            return  (bonusPercentage * bonusPoolAmount);           
        }
    }
}
