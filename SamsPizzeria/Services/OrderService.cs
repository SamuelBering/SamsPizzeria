using SamsPizzeria.Models.ViewModels;
using SamsPizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public ICollection<OrderViewModel> Orders
        {
            get
            {
                ICollection<OrderViewModel> ordersVM = _repository.Orders.Select(o =>
                new OrderViewModel
                {
                    OrderId = o.BestallningId,
                    TotalAmount = o.Totalbelopp,
                    OrderDate = o.BestallningDatum,
                    Delivered = o.Levererad
                }
                ).ToList();

                return ordersVM;
            }
        }

    }
}
