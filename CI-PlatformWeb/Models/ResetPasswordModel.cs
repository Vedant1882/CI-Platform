using System.ComponentModel.DataAnnotations;

namespace CI_PlatformWeb.Models
{
    public class ResetPasswordModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Email")]
        
        public string Email { get; set; } = null!;

        public string Token { get; set; } = null!;

       
        public string Password { get; set; } = null!;

        
        public string ConfirmPassword { get; set; } = null!;
    }
}
