using System;

namespace DateTimeIntervalsServer.Data.DomainModels
{
    public class DateTimeInterval
    {
        public int Id { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
