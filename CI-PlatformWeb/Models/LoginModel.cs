using System.ComponentModel.DataAnnotations;
namespace CI.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

    }
}
