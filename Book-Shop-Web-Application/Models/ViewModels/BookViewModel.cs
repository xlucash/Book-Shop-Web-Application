using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Book_Shop_Web_Application.Data.Base;

namespace Book_Shop_Web_Application.Models.ViewModels {
    public class BookViewModel
    {
        // Book
        public int Id { get; set; }

        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        public string Title { get; set; }

        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Opis jest wymagany")]
        public string Description { get; set; }

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Cena jest wymagana")]
        public double Price { get; set; }

        [Display(Name = "Książka (URL do zdjęcia okładki)")]
        [Required(ErrorMessage = "Zdjęcie książki jest wymagane")]
        public string ImageURL { get; set; }

        [Display(Name = "Rok publikacji")]
        [Required(ErrorMessage = "Rok publikacji jest wymagany")]
        public int YearOfPublication { get; set; }

        // Publisher
        [Display(Name = "Wybierz wydawnictwo")]
        [Required(ErrorMessage = "Wydawnictwo jest wymagane")]
        public int PublisherId { get; set; }
        // Author
        [Display(Name = "Wybierz autora")]
        [Required(ErrorMessage = "Autor jest wymagany")]
        public int AuthorId { get; set; }
    }
}
