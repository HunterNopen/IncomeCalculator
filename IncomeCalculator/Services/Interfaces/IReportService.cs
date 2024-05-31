using IncomeCalculator.Models;

namespace IncomeCalculator.Services.Interfaces
{
    public interface IReportService
    {
        EmployeeReport GetEmployeeReport(string lastName);
    }
}
