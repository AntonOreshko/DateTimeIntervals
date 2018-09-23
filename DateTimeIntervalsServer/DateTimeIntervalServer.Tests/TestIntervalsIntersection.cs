using System;
using System.Collections.Generic;
using System.Linq;
using DateTimeIntervals.Api.Helpers;
using DateTimeIntervals.DomainLayer.DomainModels;
using Xunit;

namespace DateTimeIntervals.Tests
{
    public class TestIntervalsIntersection
    {
        private readonly DateTimeInterval _targetInterval = new DateTimeInterval
        {
            Begin = new DateTime(2018, 9, 24),
            End = new DateTime(2018, 9, 28)
        };

        private readonly List<DateTimeInterval> _intervals = new List<DateTimeInterval>
        {
            // 0
            new DateTimeInterval
            {
                Begin = new DateTime(2018, 9, 21),
                End = new DateTime(2018, 9, 25)
            },
            // 1
            new DateTimeInterval
            {
                Begin = new DateTime(2018, 9, 26),
                End = new DateTime(2018, 9, 30)
            },
            // 2
            new DateTimeInterval
            {
                Begin = new DateTime(2018, 9, 25),
                End = new DateTime(2018, 9, 27)
            },
            // 3
            new DateTimeInterval
            {
                Begin = new DateTime(2018, 9, 22),
                End = new DateTime(2018, 9, 30)
            },
            // 4
            new DateTimeInterval
            {
                Begin = new DateTime(2018, 9, 20),
                End = new DateTime(2018, 9, 24)
            },
            // 5
            new DateTimeInterval
            {
                Begin = new DateTime(2018, 9, 28),
                End = new DateTime(2018, 9, 30)
            },
            // 6
            new DateTimeInterval
            {
                Begin = new DateTime(2018, 9, 20),
                End = new DateTime(2018, 9, 23)
            },
            // 7
            new DateTimeInterval
            {
                Begin = new DateTime(2018, 9, 29),
                End = new DateTime(2018, 10, 3)
            },
            // 8
            new DateTimeInterval
            {
                Begin = new DateTime(2018, 8, 21),
                End = new DateTime(2018, 8, 25)
            },
            // 9
            new DateTimeInterval
            {
                Begin = new DateTime(2018, 10, 21),
                End = new DateTime(2018, 10, 25)
            },
        };

        [Fact]
        public void TestIntersectionOfTwoIntervals()
        {
            Assert.True(IntersectionCreator.HasIntersection(_targetInterval, _intervals[0]));
            Assert.True(IntersectionCreator.HasIntersection(_targetInterval, _intervals[1]));
            Assert.True(IntersectionCreator.HasIntersection(_targetInterval, _intervals[2]));
            Assert.True(IntersectionCreator.HasIntersection(_targetInterval, _intervals[3]));
            Assert.True(IntersectionCreator.HasIntersection(_targetInterval, _intervals[4]));
            Assert.True(IntersectionCreator.HasIntersection(_targetInterval, _intervals[5]));
            Assert.False(IntersectionCreator.HasIntersection(_targetInterval, _intervals[6]));
            Assert.False(IntersectionCreator.HasIntersection(_targetInterval, _intervals[7]));
            Assert.False(IntersectionCreator.HasIntersection(_targetInterval, _intervals[8]));
            Assert.False(IntersectionCreator.HasIntersection(_targetInterval, _intervals[9]));
        }

        [Fact]
        public void TestResultIntersectionListCreation()
        {
            var resultIntersection = IntersectionCreator.CreateIntersection(_intervals, _targetInterval);

            var dateTimeIntervals = resultIntersection.ToList();

            Assert.True(dateTimeIntervals.Count == 6);

            Assert.Contains(_intervals[0], dateTimeIntervals);
            Assert.Contains(_intervals[1], dateTimeIntervals);
            Assert.Contains(_intervals[2], dateTimeIntervals);
            Assert.Contains(_intervals[3], dateTimeIntervals);
            Assert.Contains(_intervals[4], dateTimeIntervals);
            Assert.Contains(_intervals[5], dateTimeIntervals);

            Assert.DoesNotContain(_intervals[6], dateTimeIntervals);
            Assert.DoesNotContain(_intervals[7], dateTimeIntervals);
            Assert.DoesNotContain(_intervals[8], dateTimeIntervals);
            Assert.DoesNotContain(_intervals[9], dateTimeIntervals);
        }
    }
}
