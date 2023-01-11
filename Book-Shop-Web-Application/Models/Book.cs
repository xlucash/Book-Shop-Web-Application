using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Book_Shop_Web_Application.Data.Base;

namespace Book_Shop_Web_Application.Models
{
    public class Book:IEntityBase
    {
        // Book
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Cena")]
        public double Price { get; set; }

        [Display(Name = "Książka")]
        public string ImageURL { get; set; }

        [Display(Name = "Rok publikacji")]
        public int YearOfPublication { get; set; }

        // Publisher
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }

        // Author
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
