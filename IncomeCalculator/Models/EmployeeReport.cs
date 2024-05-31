using System;
using System.Collections.Generic;

namespace IncomeCalculator.Models
{
    public class EmployeeReport
    {
        public EmployeeReport()
        {
            TimeLogs = new List<TimeLog>();
        }

        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<TimeLog> TimeLogs { get; set; }

        public int TotalHours { get; set; }
        public decimal Bill { get; set; }
    }
}
