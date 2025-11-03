using RestaurantSystem.Domain.Entities;
using RestaurantSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orederrepo)
        {
            this._orderRepo = orederrepo;
        }


        public Task<IEnumerable<Order>> GetAllAsync() => _orderRepo.GetAllAsync();
        public Task<Order?> GetByIdAsync(int id) => _orderRepo.GetByIdAsync(id);
        public Task<Order?> AddAsync(Order order) =>  _orderRepo.AddAsync(order); 
        public Task UpdateAsync(Order order) => _orderRepo.UpdateAsync(order);
        public Task DeleteAsync(int id) => _orderRepo.DeleteAsunc(id);

        public async Task<bool> ProcessPaymentAsync(int orderId , string method) { 
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null) return false;

            order.PaymentMethod = method;
            order.PaymentStatus = "Paid";
            await _orderRepo.UpdateAsync(order);
            return true;
        }
    }
}
