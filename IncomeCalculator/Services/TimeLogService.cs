using IncomeCalculator.Models;
using IncomeCalculator.Repositories.Interfaces;
using IncomeCalculator.Services.Interfaces;

namespace IncomeCalculator.Services
{
    public class TimeLogService : ITimeLogService
    {
        private readonly ITimeLogRepository _timeLogRepository;

        public TimeLogService(ITimeLogRepository timeLogRepository)
        {
            _timeLogRepository = timeLogRepository;
        }

        public bool TrackTime(TimeLog timeLog)
        {
            bool isValid = timeLog.WorkingHours > 0 && timeLog.WorkingHours <= 24 && !string.IsNullOrWhiteSpace(timeLog.LastName);

            if (!isValid)
            {
                return false;
            }

            _timeLogRepository.Add(timeLog);

            return true;
        }
    }
}
