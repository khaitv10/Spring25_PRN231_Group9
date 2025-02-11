namespace BOs.ResponseModels.User
{
    public class UserInfoResponseModel
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string Password { get; set; } = null!;

        public bool? Status { get; set; }

        public string Role { get; set; } = null!;
    }
}
