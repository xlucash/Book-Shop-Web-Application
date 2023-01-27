using Book_Shop_Web_Application.Data.Base;
using Book_Shop_Web_Application.Models.ViewModels;
using Book_Shop_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Threading.Tasks;
using Book_Shop_Web_Application.Data.Interfaces;

namespace Book_Shop_Web_Application.Data.Services
{
    public class BooksService : EntityBaseRepository<Book>, IBooksService
    {
        private readonly BookDbContext _context;
        public BooksService(BookDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewBookAsync(BookViewModel data)
        {
            var newBook = new Book()
            {
                Title= data.Title,
                Description= data.Description,
                Price= data.Price,
                YearOfPublication= data.YearOfPublication,
                ImageURL= data.ImageURL,
                PublisherId= data.PublisherId,
                AuthorId= data.AuthorId,
            };

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var bookDetails = _context.Books
                .Include(p => p.Publisher)
                .Include(a => a.Author)
                .FirstOrDefaultAsync(n => n.Id == id);

            return await bookDetails;
        }

        public async Task<BookDropdownsViewModel> GetBookDropdownValues()
        {
            var response = new BookDropdownsViewModel();
            response.Publishers = await _context.Publishers.OrderBy(n => n.Name).ToListAsync();
            response.Authors = await _context.Authors.OrderBy(n => n.FullName).ToListAsync();

            return response;
        }
        public async Task UpdateBookAsync(BookViewModel data)
        {
            var dbBook = await _context.Books.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dbBook != null)
            {
                dbBook.Title = data.Title;
                dbBook.Description = data.Description;
                dbBook.Price = data.Price;
                dbBook.YearOfPublication = data.YearOfPublication;
                dbBook.ImageURL = data.ImageURL;
                dbBook.PublisherId = data.PublisherId;
                dbBook.AuthorId = data.AuthorId;
               

                await _context.SaveChangesAsync();
            }



        }
    }
}
