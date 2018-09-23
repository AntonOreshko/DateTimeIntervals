using DateTimeIntervals.Logger.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DateTimeIntervals.Logger.Data
{
    public class LoggerContext : DbContext
    {
        public DbSet<LogData> LogData { get; set; }

        public LoggerContext(DbContextOptions<LoggerContext> options) : base(options)
        {

        }
    }
}
