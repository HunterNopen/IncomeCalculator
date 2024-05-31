using IncomeCalculator.Models;
using IncomeCalculator.Services.Interfaces;

namespace IncomeCalculator.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeService _employeeRepository;
        public EmployeeService(IEmployeeService employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public bool AddEmployee(StaffEmployee staffEmployee)
        {
            bool isValid = !string.IsNullOrEmpty(staffEmployee.LastName) && staffEmployee.Salary > 0;

            if (isValid)
            {
                _employeeRepository.AddEmployee(staffEmployee);
            }

            return isValid;
        }
    }
}
