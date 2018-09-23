using System.Collections.Generic;

namespace DateTimeIntervals.DomainLayer.DomainModels
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public ICollection<DateTimeInterval> Intervals { get; set; }
    }
}
