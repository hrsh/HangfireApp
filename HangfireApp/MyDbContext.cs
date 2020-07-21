using HangfireApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace HangfireApp
{
    public class MyDbContext : DbContext
    {
        public MyDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmailEntity> Emails { get; set; }
    }
}
