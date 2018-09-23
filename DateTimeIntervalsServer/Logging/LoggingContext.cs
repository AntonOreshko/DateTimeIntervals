using Microsoft.EntityFrameworkCore;

namespace DateTimeIntervals.Logging
{
    public class LoggingContext : DbContext
    {
        public DbSet<LogData> LogData { get; set; }

        public LoggingContext(DbContextOptions<LoggingContext> options) : base(options)
        {

        }
    }
}
