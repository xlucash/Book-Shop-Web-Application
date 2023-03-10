using Book_Shop_Web_Application.Data.Base;
using Book_Shop_Web_Application.Data.Interfaces;
using Book_Shop_Web_Application.Models;

namespace Book_Shop_Web_Application.Data.Services
{
    public class PublishersService : EntityBaseRepository<Publisher>, IPublishersService
    {
        public PublishersService(BookDbContext context) : base(context)
        {

        }
    }
}
