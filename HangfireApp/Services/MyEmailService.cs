using HangfireApp.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HangfireApp.Services
{
    public interface IMyEmailService
    {
        Task<bool> SendAsync(EmailEntity model, CancellationToken ct = default);

        bool Send(EmailEntity model);
    }

    public class MyEmailService : IMyEmailService
    {
        private readonly MyDbContext _context;

        public MyEmailService(MyDbContext context)
        {
            _context = context ?? throw new NullReferenceException();
        }

        public bool Send(EmailEntity model)
        {
            // Operation to send email goes here

            // save email object to database
            _context.Add(model);
            var t = _context.SaveChanges();
            return t > 0;
        }

        public async Task<bool> SendAsync(EmailEntity model, CancellationToken ct = default)
        {
            // Operation to send email goes here


            // Save email object to database
            await _context.AddAsync(model, ct);
            var t = await _context.SaveChangesAsync(cancellationToken: ct);

            // based on method result, return true or false
            return t > 0;
        }
    }
}
