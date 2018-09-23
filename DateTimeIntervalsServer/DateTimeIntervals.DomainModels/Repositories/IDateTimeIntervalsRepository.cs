using System.Collections.Generic;
using System.Threading.Tasks;
using DateTimeIntervals.DomainLayer.DomainModels;

namespace DateTimeIntervals.DomainLayer.Repositories
{
    public interface IDateTimeIntervalsRepository
    {
        void AddInterval(DateTimeInterval interval);

        Task<DateTimeInterval> GetInterval(int id);

        Task<IEnumerable<DateTimeInterval>> GetIntervals(int userId);

        Task<User> GetUser(int id);

        Task<bool> SaveAll();
    }
}
