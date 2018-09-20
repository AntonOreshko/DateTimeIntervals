using DateTimeIntervalsServer.Data.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DateTimeIntervalsServer.Data
{
    public class DateTimeIntervalContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<DateTimeInterval> Intervals { get; set; }

        public DateTimeIntervalContext(DbContextOptions<DateTimeIntervalContext> options) : base(options)
        {

        }

    }
}
