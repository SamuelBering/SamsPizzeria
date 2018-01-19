using SamsPizzeria.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Services
{
    public interface IOrderService
    {
        Task<ICollection<OrderViewModel>> GetOrders();

        void UpdateOrderStatus(int orderId, bool status);

        void DeleteOrder(int orderId);

        Task<OrderDetailsModel> GetOrderDetails(int orderId);
        
    }
}
