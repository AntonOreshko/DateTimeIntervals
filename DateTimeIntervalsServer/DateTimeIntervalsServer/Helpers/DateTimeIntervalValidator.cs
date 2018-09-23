using DateTimeIntervals.DomainLayer.DomainModels;

namespace DateTimeIntervals.Api.Helpers
{
    public static class DateTimeIntervalValidator
    {
        public static bool Validate(DateTimeInterval interval)
        {
            return interval.End >= interval.Begin;
        }
    }
}
