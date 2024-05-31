﻿using System;


namespace IncomeCalculator.Models
{
    public class TimeLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int WorkingHours { get; set; }
        public string LastName { get; set; }

        public string Comment { get; set; }
    }
}
