using IncomeCalculator.Models;
using IncomeCalculator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IncomeCalculator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TimeLogController : Controller
    {
        private readonly ITimeLogService _timeLogService;

        public TimeLogController(ITimeLogService timeLogService)
        {
            _timeLogService = timeLogService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TrackTime(TimeLog timeLog)
        {
            if (ModelState.IsValid)
            {
                _timeLogService.TrackTime(timeLog);
                return RedirectToAction("Index");
            }
            return View("Index", timeLog);
        }
    }
}
