using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;  // ✅ Add this

namespace LMS.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
