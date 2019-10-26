using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UserForRegistrationDto
    {
        [Required]
        public string username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 8 character")]
        public string password { get; set; }
    }
}
