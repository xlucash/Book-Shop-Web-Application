using Book_Shop_Web_Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book_Shop_Web_Application.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
