using Book_Shop_Web_Application.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Book_Shop_Web_Application.Models
{
    public class Author:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Zdjęcie")]
        [Required(ErrorMessage = "Zdjęcie jest wymagane!")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Imię i nazwisko")]
        [Required(ErrorMessage = "Imię i nazwisko jest wymagane!")]
        public string FullName { get; set; }

        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Opis jest wymagany!")]
        public string Bio { get; set; }

        // relationships
        public List<Book> Books { get; set; }
    }
}
