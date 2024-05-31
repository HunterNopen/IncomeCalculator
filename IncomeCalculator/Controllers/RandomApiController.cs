using IncomeCalculator.Models;
using IncomeCalculator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IncomeCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITimeLogService _timeLogService;

        public RandomApiController(IEmployeeService employeeService, ITimeLogService timeLogService)
        {
            _employeeService = employeeService;
            _timeLogService = timeLogService;
        }

        [HttpPost("generate-random-staff-employees")]
        public IActionResult GenerateRandomStaffEmployees(int count)
        {
            var randomStaffEmployees = GenerateRandomStaffEmployeesList(count);
            foreach (var employee in randomStaffEmployees)
            {
                _employeeService.AddEmployee(employee);
            }
            return Ok(randomStaffEmployees);
        }

        [HttpPost("generate-random-time-logs")]
        public IActionResult GenerateRandomTimeLogs(int count)
        {
            var randomTimeLogs = GenerateRandomTimeLogsList(count);
            foreach (var timeLog in randomTimeLogs)
            {
                _timeLogService.TrackTime(timeLog);
            }
            return Ok(randomTimeLogs);
        }

        private List<StaffEmployee> GenerateRandomStaffEmployeesList(int count)
        {
            var random = new Random();
            var staffEmployees = new List<StaffEmployee>();
            var lastNames = new[] { "Smith", "Johnson", "Williams", "Jones", "Brown" };

            for (int i = 0; i < count; i++)
            {
                var employee = new StaffEmployee
                {
                    Salary = (decimal)(random.Next(40000, 100000) + random.NextDouble()),
                    LastName = lastNames[random.Next(lastNames.Length)]
                };
                staffEmployees.Add(employee);
            }

            return staffEmployees;
        }

        private List<TimeLog> GenerateRandomTimeLogsList(int count)
        {
            var random = new Random();
            var timeLogs = new List<TimeLog>();
            var lastNames = new[] { "Smith", "Johnson", "Williams", "Jones", "Brown" };
            var comments = new[] { "Worked on project A", "Completed task B", "Meeting with team", "Client presentation", "Training session" };

            for (int i = 0; i < count; i++)
            {
                var timeLog = new TimeLog
                {
                    Date = DateTime.Now.AddDays(-random.Next(1, 30)),
                    WorkingHours = random.Next(1, 12),
                    LastName = lastNames[random.Next(lastNames.Length)],
                    Comment = comments[random.Next(comments.Length)]
                };
                timeLogs.Add(timeLog);
            }

            return timeLogs;
        }
    }
}
