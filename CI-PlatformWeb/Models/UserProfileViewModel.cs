using CI_Entity.Models;
using System.ComponentModel.DataAnnotations;

namespace CI_PlatformWeb.Models
{
    public class UserProfileViewModel
    {
        [Required(ErrorMessage = "First Name is a Required field.")]
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "FirstName")]
        [RegularExpression("^((?!^First Name$)[a-zA-Z '])+$", ErrorMessage = "First name  must be properly formatted.")]
        public string firstname { get; set; }
        [Required(ErrorMessage = "Last Name is a Required field.")]
        [DataType(DataType.Text)]
        [Display(Order = 1, Name = "LastName")]
        [RegularExpression("^((?!^First Name$)[a-zA-Z '])+$", ErrorMessage = "Last name  must be properly formatted.")]
        public string lastname { get; set; }
        [Required(ErrorMessage = "EmployeeId is a Required field.")]
        public string employeeid { get; set; }

        public string email { get; set; }
        public string oldpassword { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public string whyivolunteered { get; set; }

        public string myprofile { get; set; }
        public string title { get; set; }
        public string manager { get; set; }
        public string department { get; set; }
        public string availability { get; set; }

        public string linkedinurl { get; set; }

        public IFormFile files { get; set; }
        public string avatar { get; set; }
        public string username { get; set; }
        [Required(ErrorMessage = "Subject is a Required field.")]
        public string subject { get; set; }
        [Required(ErrorMessage = "Message is a Required field.")]
        public string message { get; set; }
        
        public long? cityid { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country Is Required")]
        public long? countryid { get; set; }

        public List<Skill> allskills { get; set; }  


    }
}
