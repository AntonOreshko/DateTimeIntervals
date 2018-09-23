using System;

namespace DateTimeIntervals.Dtos.Dtos
{
    public class RequestDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ResponseCode { get; set; }

        public string RequestMethod { get; set; }

        public string RequestPath { get; set; }

        public DateTime RequestTime { get; set; }

        public DateTime ResponseTime { get; set; }

        public string RequestProtocol { get; set; }

        public string RequestBody { get; set; }

        public string ResponseBody { get; set; }
    }
}
