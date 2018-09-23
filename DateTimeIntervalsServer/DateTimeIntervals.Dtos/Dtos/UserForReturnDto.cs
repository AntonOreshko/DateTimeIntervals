using System.ComponentModel.DataAnnotations;

namespace DateTimeIntervals.Dtos.Dtos
{
    public class UserForReturnDto
    {
        public string Login { get; set; }

        public int Id { get; set; }

        public string Token { get; set; }
    }
}
