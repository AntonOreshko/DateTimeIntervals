using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DateTimeIntervalsServer.Data.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DateTimeIntervalsServer.Data.Repositories
{
    public class DateTimeIntervalsRepository: IDateTimeIntervalsRepository
    {
        private readonly DateTimeIntervalContext _context;

        public DateTimeIntervalsRepository(DateTimeIntervalContext context)
        {
            _context = context;
        }

        public void AddInterval(DateTimeInterval interval)
        {
            _context.Add(interval);
        }

        public async Task<DateTimeInterval> GetInterval(int id)
        {
            var interval = await _context.Intervals.FirstOrDefaultAsync(i => i.Id == id);

            return interval;
        }

        public async Task<IEnumerable<DateTimeInterval>> GetIntervals(int userId)
        {
            var intervals = await _context.Intervals.Where(i => i.UserId == userId).ToListAsync();

            return intervals;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }
    }
}
