using Book_Shop_Web_Application.Models;
using System.Collections.Generic;

namespace Book_Shop_Web_Application.Data.ViewModels
{
    public class BookDropdownsViewModel
    {
        public BookDropdownsViewModel() {
            Publishers = new List<Publisher>();
            Authors = new List<Author>();
        }
        public List<Publisher> Publishers { get; set; }
        public List<Author> Authors { get; set; }
    }
}
