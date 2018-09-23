using System;

namespace DateTimeIntervals.Dtos.Dtos
{
    public class DateTimeIntervalDto
    {
        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public override string ToString()
        {
            return Begin.Date.ToString("yyyy-MM-dd") + " / " + End.Date.ToString("yyyy-MM-dd");
        }
    }
}
