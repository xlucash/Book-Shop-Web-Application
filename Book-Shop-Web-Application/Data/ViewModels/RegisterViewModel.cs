using System.ComponentModel.DataAnnotations;

namespace Book_Shop_Web_Application.Data.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Imię i nazwisko")]
        [Required(ErrorMessage = "Imię i nazwisko jest wymagane!")]
        public string FullName { get; set; }

        [Display(Name = "Adres email")]
        [Required(ErrorMessage = "Adres email jest wymagany!")]
        public string EmailAddress { get; set; }

        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Powtórz hasło")]
        [Required(ErrorMessage = "Potwierdzenie hasłą jest wymagane!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła różnią się!")]
        public string ConfirmPassword { get; set; }
    }
}
