using System.ComponentModel.DataAnnotations;

namespace CI_PlatformWeb.Models
{
    public class ResetPasswordModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide Email")]
        
        public string Email { get; set; } = null!;

        public string Token { get; set; } = null!;


        [Required]        [DataType(DataType.Password)]        [MinLength(8, ErrorMessage = "Password should contain atleast 8 charachter")]        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password should contain atleast one Capital letter , one small case letter, one Digit and one special symbol")]        public string Password { get; set; }        [Required]        [DataType(DataType.Password)]        [Display(Name = "Confirm Password")]        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]        public string ConfirmPassword
        {
            get; set;

        }
    }
}
