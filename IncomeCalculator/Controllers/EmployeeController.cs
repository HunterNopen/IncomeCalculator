using IncomeCalculator.Models;
using IncomeCalculator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IncomeCalculator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(StaffEmployee staffEmployee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.AddEmployee(staffEmployee);
                return RedirectToAction("Index");
            }
            return View("Index", staffEmployee);
        }
    }
}
