﻿

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide First Name")]
        //[Remote(action: "EmailAlreadyExists", controller: "Login")]
        public string Email { get; set; } = null!;
        //[StringLength(10, ErrorMessage = "Phone number is invalid")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Phone Number not valid")]
        {
            get; set;

        }
    }
}