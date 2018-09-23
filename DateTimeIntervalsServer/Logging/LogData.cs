namespace DateTimeIntervals.Logging
{
    public class LogData
    {
        public int Id { get; set; }

        public string RemoteAddr { get; set; }

        public string User { get; set; }

        public int ResponseStatus { get; set; }

        public string RequestMethod { get; set; }

        public string RequestTimestamp { get; set; }

        public string RequestPath { get; set; }

        public string RequestProtocol { get; set; }

        public string UserAgent { get; set; }

        public long DurationMs { get; set; }
    }
}