using System.Threading.Tasks;
using DateTimeIntervals.Logger.Data;
using DateTimeIntervals.Logger.DomainModels;

namespace DateTimeIntervals.Logger.Repositories
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly LoggerContext _context;

        public LoggerRepository(LoggerContext context)
        {
            _context = context;
        }

        public void AddLogData(LogData logData)
        {
            _context.Add(logData);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
