using System.ComponentModel.DataAnnotations;

namespace CI_PlatformWeb.Models
{
    public class ForgetPass
    {
        [Required(ErrorMessage = "please Enter Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,3}$", ErrorMessage = "Please Provide Valid Email")]
        public string? Email { get; set; }

    }
}