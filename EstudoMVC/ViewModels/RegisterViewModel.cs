using System.ComponentModel.DataAnnotations;

namespace EstudoMVC.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email addres")]
        [Required(ErrorMessage = "Email addres required")]
        public string EmailAddress { get; set; }
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Confirm passaword")]
        [Required(ErrorMessage ="Confirm password is required")]
        [Compare("Password", ErrorMessage ="Password does not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = "Birth date is required")]
        public DateTime Birth { get; set; }
    }
}
