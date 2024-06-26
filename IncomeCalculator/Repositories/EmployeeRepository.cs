﻿using IncomeCalculator.Models;
using IncomeCalculator.Repositories.Interfaces;
using Timesheet.DataAccess.csv;

namespace IncomeCalculator.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly char _delimeter;
        private readonly string _path;

        public EmployeeRepository(CsvSettings csvSettings)
        {
            _delimeter = csvSettings.Delimeter;
            _path = csvSettings.Path + "\\employees.csv";
        }

        public void AddEmployee(StaffEmployee staffEmployee)
        {
            var dataRow = $"{staffEmployee.LastName}{_delimeter}{staffEmployee.Salary}{_delimeter}\n";
            File.AppendAllText(_path, dataRow);
        }

        public StaffEmployee GetEmployee(string lastName)
        {
            var data = File.ReadAllText(_path);
            var dataRows = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            StaffEmployee staffEmployee = null;

            foreach (var dataRow in dataRows)
            {
                if (dataRow.Contains(lastName))
                {
                    var dataMembers = dataRow.Split(_delimeter);

                    staffEmployee = new StaffEmployee
                    {
                        LastName = dataMembers[0],
                        Salary = decimal.TryParse(dataMembers[1], out decimal salary) ? salary : 0
                    };

                    break;
                }
            }

            return staffEmployee;
        }
    }
}
