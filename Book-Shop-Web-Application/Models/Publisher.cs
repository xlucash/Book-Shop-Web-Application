using Book_Shop_Web_Application.Data.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Book_Shop_Web_Application.Models
{
    public class Publisher : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Logo")]
        [Required(ErrorMessage = "Logo jest wymagane!")]
        public string LogoPictureURL { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa jest wymagana!")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Opis jest wymagany!")]
        public string Description { get; set; }

        // relationships
        public List<Book> Books { get; set; }
    }
}
