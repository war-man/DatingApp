using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserToRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength:8,MinimumLength=4,ErrorMessage="Password must be between 4 and 8 Character")]
        public string Password { get; set; }
    }
}