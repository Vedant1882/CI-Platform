namespace CI_PlatformWeb.Models
{
    public class ResetPasswordModel
    {
        public string Email { get; set; } = null!;

        public string Token { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}
