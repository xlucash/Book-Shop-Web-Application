using Book_Shop_Web_Application.Data.Base;
using Book_Shop_Web_Application.Data.ViewModels;
using Book_Shop_Web_Application.Models;
using System.Threading.Tasks;

namespace Book_Shop_Web_Application.Data.Services
{
    public interface IBooksService:IEntityBaseRepository<Book>
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<BookDropdownsViewModel> GetBookDropdownValues();

        Task AddNewBookAsync(BookViewModel data);
        Task UpdateBookAsync(BookViewModel data);
    }
}
