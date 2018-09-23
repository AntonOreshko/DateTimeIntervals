using System.ComponentModel.DataAnnotations;

namespace DateTimeIntervals.Dtos.Dtos
{
    public class UserForLoginDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Your must specified password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}
