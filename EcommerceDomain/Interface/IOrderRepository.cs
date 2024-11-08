using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetById(Guid id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task Delete(Guid id);
    }
}