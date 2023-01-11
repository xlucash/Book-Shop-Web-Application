using Book_Shop_Web_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Shop_Web_Application.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
    }
}
