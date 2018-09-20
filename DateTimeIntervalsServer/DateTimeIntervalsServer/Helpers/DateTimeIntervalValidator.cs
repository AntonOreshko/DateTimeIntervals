using DateTimeIntervalsServer.Data.DomainModels;

namespace DateTimeIntervalsServer.Helpers
{
    public static class DateTimeIntervalValidator
    {
        public static bool Validate(DateTimeInterval interval)
        {
            return interval.End >= interval.Begin;
        }
    }
}
