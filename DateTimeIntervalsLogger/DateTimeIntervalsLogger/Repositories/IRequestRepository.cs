using DateTimeIntervals.Dtos.Dtos;

namespace DateTimeIntervalsLogger.Repositories
{
    public interface IRequestRepository
    {
        void AddRequestData(RequestDto data);
    }
}
