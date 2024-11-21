using Microsoft.EntityFrameworkCore;
using ScheduleService.Models;

namespace ScheduleService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Schedule> Schedules { get; set; }
    }
}
