using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HangfireApp.Models;
using HangfireApp.Services;
using Hangfire;
using System.Threading;

namespace HangfireApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMyEmailService _emailService;

        public HomeController(IMyEmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendEmailOnce([Bind]EmailEntity model, CancellationToken ct)
        {
            BackgroundJob.Enqueue(() => _emailService.SendAsync(model, ct));
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendEmailAfterward([Bind] EmailEntity model, string time, CancellationToken ct)
        {
            // BackgroundJob.Schedule(() => _emailService.SendAsync(model, ct), TimeSpan.FromMinutes(1));

            var dummyMinutes = int.Parse(time);
            BackgroundJob.Schedule(() => _emailService.SendAsync(model, ct), TimeSpan.FromMinutes(dummyMinutes));
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendEmailDaily([Bind] EmailEntity model, CancellationToken ct)
        {
            RecurringJob.AddOrUpdate(() => _emailService.SendAsync(model, ct), Cron.Daily);
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendEmailEveryXDay([Bind] EmailEntity model, string time, CancellationToken ct)
        {
            var dummyDays = int.Parse(time);
            RecurringJob.AddOrUpdate(() => _emailService.SendAsync(model, ct), Cron.DayInterval(dummyDays));
            return Ok();
        }
    }
}
