using System.Collections.Generic;
using DateTimeIntervals.DomainLayer.DomainModels;

namespace DateTimeIntervals.Api.Helpers
{
    public static class IntersectionCreator
    {
        public static IEnumerable<DateTimeInterval> CreateIntersection(IEnumerable<DateTimeInterval> intervals, DateTimeInterval targetInterval)
        {
            var intersection = new List<DateTimeInterval>();

            foreach (var interval in intervals)
            {
                if (HasIntersection(targetInterval, interval))
                {
                    intersection.Add(interval);
                }
            }

            return intersection;
        }

        public static bool HasIntersection(DateTimeInterval target, DateTimeInterval compare)
        {
            return (compare.Begin <= target.End && target.Begin <= compare.End);
        }
    }
}
