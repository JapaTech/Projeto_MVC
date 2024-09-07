using System.ComponentModel.DataAnnotations;

namespace EstudoMVC.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Addres")]
        [Required(ErrorMessage = "Email required")]
        public string EmailAddress { get; set; }
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } 
    }
}
