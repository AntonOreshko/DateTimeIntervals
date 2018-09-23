using System.Threading.Tasks;
using DateTimeIntervals.Logger.DomainModels;

namespace DateTimeIntervals.Logger.Repositories
{
    public interface ILoggerRepository
    {
        void AddLogData(LogData logData);

        Task<bool> SaveAll();
    }
}
