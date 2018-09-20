using System.Collections.Generic;
using DateTimeIntervalsServer.Data.DomainModels;

namespace DateTimeIntervalsServer.Helpers
{
    public static class IntersectionCreator
    {
        public static IEnumerable<DateTimeInterval> CreateIntersection(IEnumerable<DateTimeInterval> intervals, DateTimeInterval targetInterval)
        {
            var intersection = new List<DateTimeInterval>();

            foreach (var interval in intervals)
            {
                if (interval.Begin <= targetInterval.End && targetInterval.Begin <= interval.End)
                {
                    intersection.Add(interval);
                }
            }

            return intersection;
        }
    }
}
