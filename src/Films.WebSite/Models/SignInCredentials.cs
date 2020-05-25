using System.ComponentModel.DataAnnotations;

namespace Films.WebSite.Models
{
    public class SignInCredentials
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
