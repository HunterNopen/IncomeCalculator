using IncomeCalculator.Models;

namespace IncomeCalculator.Services.Interfaces
{
    public interface ITimeLogService
    {
        bool TrackTime(TimeLog timeLog);
    }
}

