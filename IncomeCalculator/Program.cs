using IncomeCalculator.Repositories;
using IncomeCalculator.Repositories.Interfaces;
using IncomeCalculator.Services;
using IncomeCalculator.Services.Interfaces;

namespace IncomeCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<ITimeLogRepository, TimeLogRepository>();
            builder.Services.AddTransient<ITimeLogService, TimeLogService>();
            builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<IReportService, ReportService>();

            builder.Services.AddSingleton(x => new CsvSettings(';', "..\\Timesheet.DataAccess.csv\\Data"));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
