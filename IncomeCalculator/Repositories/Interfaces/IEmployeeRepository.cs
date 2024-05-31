using IncomeCalculator.Models;

namespace IncomeCalculator.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        StaffEmployee GetEmployee(string lastName);
        void AddEmployee(StaffEmployee staffEmployee);
    }
}
