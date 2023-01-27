using Book_Shop_Web_Application.Models;
using Book_Shop_Web_Application.Models.ViewModels;
using System.Threading.Tasks;

namespace Book_Shop_Web_Application.Data.Interfaces
{
    public interface IBooksService : IEntityBaseRepository<Book>
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<BookDropdownsViewModel> GetBookDropdownValues();

        Task AddNewBookAsync(BookViewModel data);
        Task UpdateBookAsync(BookViewModel data);
    }
}
