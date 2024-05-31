using IncomeCalculator.Models;

namespace IncomeCalculator.Repositories.Interfaces
{
    public interface ITimeLogRepository
    {
        TimeLog[] GetTimeLogs(string lastName);
        void Add(TimeLog timeLog);
    }
}
