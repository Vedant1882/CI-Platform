using System.ComponentModel.DataAnnotations;
namespace CI.Models
{
    public class Login
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,3}$", ErrorMessage = "Please Provide Valid Email")]
        
        public string? Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        
        public string? Password { get; set; }

    }
}
