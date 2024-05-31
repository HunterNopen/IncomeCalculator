using IncomeCalculator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IncomeCalculator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Report(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                ModelState.AddModelError("lastName", "Last name is required.");
                return View("Index");
            }

            var report = _reportService.GetEmployeeReport(lastName);
            if (report == null)
            {
                ModelState.AddModelError("lastName", "Report not found.");
                return View("Index");
            }

            return View("Report", report);
        }
    }
}
