using IncomeCalculator.Models;
using IncomeCalculator.Repositories.Interfaces;
using IncomeCalculator.Repositories;
using IncomeCalculator.Services.Interfaces;

namespace IncomeCalculator.Services
{
    public class ReportService : IReportService
    {
        private const decimal MAX_WORKING_HOURS_PER_MONTH = 160;
        private const decimal MAX_WORKING_HOURS_PER_DAY = 8;
        private const decimal BONUS_FOR_MANAGER = 20000;

        private readonly ITimeLogRepository _timeLogRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ReportService(ITimeLogRepository timeLogRepository, IEmployeeRepository employeeRepository)
        {
            _timeLogRepository = timeLogRepository;
            _employeeRepository = employeeRepository;
        }

        public EmployeeReport GetEmployeeReport(string lastName)
        {
            var employee = _employeeRepository.GetEmployee(lastName);
            var timeLogs = _timeLogRepository.GetTimeLogs(employee.LastName);

            if (timeLogs == null || timeLogs.Length == 0)
            {
                return new EmployeeReport
                {
                    LastName = employee.LastName
                };
            }

            var totalHours = timeLogs.Sum(x => x.WorkingHours);
            decimal bill = 0;

            switch (lastName)
            {
                case "Staff":
                    {
                        var workingHoursGroupsByDay = timeLogs
                                                .GroupBy(x => x.Date.ToShortDateString());

                        foreach (var workingLogsPerDay in workingHoursGroupsByDay)
                        {
                            int dayHours = workingLogsPerDay.Sum(x => x.WorkingHours);

                            if (dayHours > MAX_WORKING_HOURS_PER_DAY)
                            {
                                var overtime = dayHours - MAX_WORKING_HOURS_PER_DAY;

                                bill += MAX_WORKING_HOURS_PER_DAY / MAX_WORKING_HOURS_PER_MONTH * employee.Salary;
                                bill += overtime / MAX_WORKING_HOURS_PER_MONTH * employee.Salary * 2;
                            }
                            else
                            {
                                bill += dayHours / MAX_WORKING_HOURS_PER_MONTH * employee.Salary;
                            }
                        }

                        break;
                    }

                case "Manager":
                    {
                        var workingHoursGroupsByDay = timeLogs
                                                .GroupBy(x => x.Date.ToShortDateString());

                        foreach (var workingLogsPerDay in workingHoursGroupsByDay)
                        {
                            int dayHours = workingLogsPerDay.Sum(x => x.WorkingHours);

                            if (dayHours > MAX_WORKING_HOURS_PER_DAY)
                            {

                                decimal bonusPerDay = MAX_WORKING_HOURS_PER_DAY / MAX_WORKING_HOURS_PER_MONTH * BONUS_FOR_MANAGER;
                                bill += MAX_WORKING_HOURS_PER_DAY / MAX_WORKING_HOURS_PER_MONTH * employee.Salary + bonusPerDay;
                            }
                            else
                            {
                                bill += dayHours / MAX_WORKING_HOURS_PER_MONTH * employee.Salary;
                            }
                        }

                        break;
                    }

                case "Freelancer":
                    {
                        bill = totalHours * employee.Salary;
                        break;
                    }

                default:
                    break;
            }

            return new EmployeeReport
            {
                LastName = employee.LastName,
                TimeLogs = timeLogs.ToList(),
                Bill = bill,
                TotalHours = totalHours,
                StartDate = timeLogs.Select(t => t.Date).Min(),
                EndDate = timeLogs.Select(t => t.Date).Max()
            };
        }


    }
}
