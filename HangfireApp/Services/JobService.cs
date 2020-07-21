using HangfireApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangfireApp.Services
{
    public interface IJobService
    {
        Task<List<JobModel>> GetAsync();
    }

    public class JobService : IJobService
    {
        private readonly MyDbContext _context;

        public JobService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<JobModel>> GetAsync()
        {
            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "select * from [HangFire].[Job]";
            command.CommandType = CommandType.Text;

            await _context.Database.OpenConnectionAsync();

            using var result = command.ExecuteReader();
            var entities = new List<JobModel>();

            while (result.Read())
            {
                entities.Add(new JobModel
                {
                    Id = (long)result[0],
                    InvocationData = result[3].ToString(),
                    Arguments = result[4].ToString()
                });
            }

            return entities;
        }
    }
}
