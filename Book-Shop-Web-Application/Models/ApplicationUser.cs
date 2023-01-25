using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Book_Shop_Web_Application.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Display(Name= "Imię i nazwisko")]
        public string FullName { get; set; }
    }
}
