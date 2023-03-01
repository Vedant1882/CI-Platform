using System.ComponentModel.DataAnnotations;

namespace CI_PlatformWeb.Models
{
    public class ForgetPass
    {
        [Required(ErrorMessage = "please enter password")]
        public string? Email { get; set; }

    }
}