using System.ComponentModel.DataAnnotations;

namespace BOs.RequestModels.Auth
{
    public class RegisterRequest
    {
        [Required]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Full name can only contain letters and spacssses.")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
