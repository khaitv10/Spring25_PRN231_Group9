using System.ComponentModel.DataAnnotations;

namespace BOs.RequestModels.User
{
    public class UserInfoUpdateModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "First name cannot contain numbers or special characters")]
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Incorrect email format")]
        public string Email { get; set; } = null!;

    }
}
