using SamsPizzeria.Models.ViewModels;
using SamsPizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace SamsPizzeria.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _repository;
        private UserManager<AppUser> _userManager;

        public OrderService(IOrderRepository repository, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public void UpdateOrderStatus(int orderId, bool status)
        {
            _repository.UpdateOrderStatus(orderId, status);
        }

        public async Task<ICollection<OrderViewModel>> GetOrders()
        {
            ICollection<OrderViewModel> orderListVM = new List<OrderViewModel>();

            foreach (var o in _repository.Orders)
            {
                var orderVM = new OrderViewModel
                {
                    OrderId = o.BestallningId,
                    TotalAmount = o.Totalbelopp,
                    OrderDate = o.BestallningDatum,
                    Delivered = o.Levererad,
                    CustomerId = o.UserId,
                    OrderStatusSelectList = new List<SelectListItem>
                    {
                        new SelectListItem
                        {
                            Text="Ja",
                            Value="true",
                            Selected = o.Levererad ? true:false
                        },
                        new SelectListItem
                        {
                            Text="Nej",
                            Value="false",
                            Selected = !o.Levererad ? true:false
                        }
                    }

                };

                var appUser = await _userManager.FindByIdAsync(o.UserId);
                orderVM.CustomerName = $"{appUser.FirstName} {appUser.LastName}";

                orderListVM.Add(orderVM);
            }

            return orderListVM;
        }

        public void DeleteOrder(int orderId)
        {
            _repository.DeleteOrder(orderId);
        }

        public async Task<OrderDetailsModel> GetOrderDetails(int orderId)
        {
            var order = _repository.Orders.Single(o => o.BestallningId == orderId);

            OrderDetailsModel orderVM = new OrderDetailsModel
            {
                Dishes = order.BestallningMatratt.Select(od =>
                    new DishDetailsModel
                    {
                        Name = od.Matratt.MatrattNamn,
                        Quantity = od.Antal,
                        Price = od.Matratt.Pris * od.Antal
                    }).ToList(),
                Discount = order.BestallningMatratt.Sum(od => od.Matratt.Pris * od.Antal) - order.Totalbelopp,
                TotalAmount = order.Totalbelopp,
                UserId = order.UserId,
                UserName = (await _userManager.FindByIdAsync(order.UserId)).UserName
            };

            return orderVM;
        }


    }
}




