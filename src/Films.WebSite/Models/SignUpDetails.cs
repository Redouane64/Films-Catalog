using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Films.WebSite.Models
{
    public class SignUpDetails
    {
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "E-mail")]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Repeat Password")]
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
